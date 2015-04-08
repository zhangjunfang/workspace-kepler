/**
 * 
 */
package org.storevm.toolkits.component;

/**
 * 组件生命周期接口定义
 * 
 * @author  ocean
 * @version $Id: LifeCycle.java, v 0.1 2010-12-29 下午08:50:10  ocean Exp $
 */
public interface LifeCycle {
    /**
     * 组件启动，用于组件的初始化
     * @throws Exception
     */
    public void start() throws Exception;

    /**
     * 组件停止，用于组件的销毁
     * @throws Exception
     */
    public void stop() throws Exception;

    /**
     * 是否已启动
     * @return
     */
    public boolean isStarted();

    /**
     * 是否已停止
     * @return
     */
    public boolean isStopped();
}
