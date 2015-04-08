#ifndef _TIME_QUEUE_H_
#define _TIME_QUEUE_H_ 1

#include <Mutex.h>

#include <map>
using std::map;
#include <list>
using std::list;

#include <time.h>


template <class K, class V>
class TimeQueue {
	struct TimeDot {
		K key;
		time_t tv;
	};

	map<K,V>        _objects;
	list<TimeDot>   _timeDot;
	int             _interval;
	share::Mutex    _mutex;
public:
	TimeQueue() : _interval(0) {
	}

	void init(int interval) {
		_interval = interval;
	}

	bool insert(const K &key, const V &val) {
		pair<typename map<K,V>::iterator, bool> ret;

		share::Guard guard(_mutex);

		ret = _objects.insert(make_pair<K,V>(key, val));
		if(ret.second == false) {
			return false;
		}

		TimeDot dot = {key, time(NULL) + _interval};
		_timeDot.push_back(dot);

		return true;
	}

	bool erase(const K &key, V &val) {
		typename map<K, V>::iterator it;

		share::Guard guard(_mutex);

		it = _objects.find(key);
		if(it == _objects.end()) {
			return false;
		}
		val = it->second;
		_objects.erase(it);

		return true;
	}

	void check(list<V> &vals) {
		typename map<K, V>::iterator it;
		time_t tv = time(NULL);

		share::Guard guard(_mutex);

		while( ! _timeDot.empty()) {
			TimeDot &dot = _timeDot.front();
			if(tv < dot.tv) {
				break;
			}

			it = _objects.find(dot.key);
			if(it != _objects.end()) {
				vals.push_back(it->second);
				_objects.erase(it);
			}

			_timeDot.pop_front();
		}
	}
};

#endif//_TIME_QUEUE_H_
