import com.lmax.disruptor.BatchEventProcessor;
import com.lmax.disruptor.RingBuffer;
import com.lmax.disruptor.SequenceBarrier;
import com.lmax.disruptor.SingleThreadedClaimStrategy;
import com.lmax.disruptor.YieldingWaitStrategy;

public class PersonHelper {
    private  static PersonHelper instance;
    private static boolean inited = false;
    /**
     * ringBuffer的容量，必须是2的N次方
     */
    private static final int BUFFER_SIZE = 256;
    private RingBuffer<PersonEvent> ringBuffer;
    private SequenceBarrier sequenceBarrier;
    private PersonEventHandler handler;
    private BatchEventProcessor<PersonEvent> batchEventProcessor; 
    
    public PersonHelper(){
        //参数1 事件
        //参数2 单线程使用
        //参数3 等待策略
    	//ringBuffer =RingBuffer.createSingleProducer(PersonEvent.EVENT_FACTORY, BUFFER_SIZE);
        ringBuffer = new RingBuffer<PersonEvent>(PersonEvent.EVENT_FACTORY, 
                new SingleThreadedClaimStrategy(BUFFER_SIZE), new YieldingWaitStrategy());
        //获取生产者的位置信息
        sequenceBarrier = ringBuffer.newBarrier();
        //消费者
        handler=new PersonEventHandler();
        //事件处理器，监控指定ringBuffer,有数据时通知指定handler进行处理
        batchEventProcessor = new BatchEventProcessor<PersonEvent>(ringBuffer, sequenceBarrier, handler);
        //传入所有消费者线程的序号
        ringBuffer.setGatingSequences(batchEventProcessor.getSequence());
        
    }
    
    /**
     * 启动消费者线程，实际上调用了AudioDataEventHandler中的onEvent方法进行处理
     */
    public static void start(){
        instance = new PersonHelper();
        Thread thread = new Thread(instance.batchEventProcessor);
        thread.start();
        inited = true;
    }
    /**
     * 停止
     */
    public static void shutdown(){
         if(!inited){
             throw new RuntimeException("EncodeHelper还没有初始化！");
         }else{
             instance.doHalt();
         }     
    }
    private void doHalt() {
        batchEventProcessor.halt();
    }
    private void doProduce(Person person){
        //获取下一个序号
        long sequence = ringBuffer.next();
        //写入数据
        ringBuffer.get(sequence).setPerson(person);
        //通知消费者该资源可以消费了
        ringBuffer.publish(sequence);
    }
    /**
     * 生产者压入生产数据
     * @param data
     */
    public static void produce(Person person){
        if(!inited){
            throw new RuntimeException("EncodeHelper还没有初始化！");
        }else{
            instance.doProduce(person);
        }
    }
    
}