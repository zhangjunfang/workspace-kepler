#include "amqclient.h"
#include <tools.h>
#include <comlog.h>
#include <netutil.h>
#include <intercoder.h>

#include "../tools/utils.h"

#include <activemq/core/ActiveMQConnectionFactory.h>
#include <activemq/library/ActiveMQCPP.h>
#include <cms/Connection.h>
#include <cms/Session.h>
#include <cms/TextMessage.h>
#include <cms/ExceptionListener.h>
#include <cms/MessageListener.h>
#include <decaf/util/concurrent/CountDownLatch.h>

using namespace activemq;
using namespace activemq::core;
using namespace cms;
using namespace decaf::util::concurrent;

#include <string>
using std::string;
#include <vector>
using std::vector;
#include <fstream>
using std::ifstream;

class BaseTabListen : public MessageListener {
	ISystemEnv  *_pEnv;
	uint16_t _msgid;
public:
	BaseTabListen(ISystemEnv *pEnv, const string &msgid)
	{
		_pEnv = pEnv;
		Utils::str2int(msgid, _msgid, ios::hex);
	}

    virtual void onMessage(const Message* message)
    {
        const TextMessage* textMessage = dynamic_cast<const TextMessage*>(message);
        if( textMessage == NULL ) {
            return;
        }
        string text = textMessage->getText();

        string::size_type i;
        vector<string> fields;
        vector<uint8_t> msgbuf;

        uint32_t seqid;

		OUT_RECV("", 0, "MQBT", "%s", text.c_str());

		char arr[1024 * 128] = { 0 };

		Utils::splitStr(text, fields, ';');
		for (i = 0; i < fields.size(); ++i) {
			msgbuf.clear();
			seqid = _pEnv->GetProtParse()->buildExchBaseTable(fields[i], _msgid, msgbuf);
			if (msgbuf.empty()) {
				continue;
			}

			if (!_pEnv->GetExchClient()->HandleData(seqid, (char*) &msgbuf[0], msgbuf.size())) {
				continue;
			}
		}
    }
};

AmqClient::AmqClient(void)
{
	_pEnv = NULL;
}

AmqClient::~AmqClient(void)
{
	_recv_thread.stop();
}

bool AmqClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char buf[1024] = {0};

	if (!_pEnv->GetString("amq_broker_url", buf)) {
		OUT_ERROR(NULL, 0, "conf", "amq_broker_url empty");
		return false;
	}
	_brokerUrl = buf;

	if (!_pEnv->GetString("amq_topic_file", buf)) {
		OUT_ERROR(NULL, 0, "conf", "amq_topic_file empty");
		return false;
	}
	_topicFile = buf;

	return true;
}

void AmqClient::Stop(void)
{
	OUT_INFO(NULL, 0, "AmqClient", "stop");

	_recv_thread.stop();
}

bool AmqClient::Start(void)
{
	if (!_recv_thread.init(1, NULL, this)) {
		printf("start amq recv thread failed, %s:%d", __FILE__, __LINE__);
		return false;
	}
	_recv_thread.start();

	return true;
}

void AmqClient::run(void *ptr)
{
	CountDownLatch latch(1);
	Connection *connection = NULL;
	Session *session = NULL;
	Destination *destination = NULL;
	MessageConsumer *consumer = NULL;

	ifstream ifs;
	string line;
	vector<string> fields;

	activemq::library::ActiveMQCPP::initializeLibrary();
	auto_ptr<ConnectionFactory> connectionFactory(ConnectionFactory::createCMSConnectionFactory(_brokerUrl.c_str()));

	do {
		try {
			connection = connectionFactory->createConnection();
		} catch (...) {
			connection = NULL;
			sleep(3);
		}
	} while (connection == NULL);
	connection->start();

	session = connection->createSession(Session::AUTO_ACKNOWLEDGE);

	ifs.open(_topicFile.c_str());
	while(getline(ifs, line)) {
		if(line.empty() || line[0] == '#') {
			continue;
		}

		fields.clear();
		if(Utils::splitStr(line, fields, ':') != 2) {
			continue;
		}

		BaseTabListen *baseTab = new BaseTabListen(_pEnv, fields[0]);
		destination = session->createQueue(fields[1].c_str());
		consumer = session->createConsumer(destination);
		consumer->setMessageListener(baseTab);
	}

	latch.await();
	activemq::library::ActiveMQCPP::shutdownLibrary();
}
