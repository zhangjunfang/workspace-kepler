
#ifndef __SINGLETON_H__
#define __SINGLETON_H__

#include <memory>
//#include "threadmutex.h"

using namespace std;

//是否启用锁
#define  __USE_THREAD__

#ifdef __USE_THREAD__
#include <pthread.h>

#define THREAD_LOCK(tThreadMutex)     tThreadMutex.Lock()
#define THREAD_UNLOCK(tThreadMutex)   tThreadMutex.UnLock()
#else
#define THREAD_LOCK(tThreadMutex)     
#define THREAD_UNLOCK(tThreadMutex)   
#endif

#define DECLARE_SINGLETON_CLASS( type ) \
	friend class auto_ptr< type >;  \
friend class CSingleton< type >;


class CThreadMutex
{
public:
#ifndef __USE_THREAD__
    CThreadMutex(){};
    ~CThreadMutex(){};
    void Lock(){};
    void UnLock(){};
#else
	CThreadMutex()
	{        
#ifdef WIN32
		InitializeCriticalSection(&m_tThreadMutex);
#else
// 		pthread_mutexattr_t attr;
// 		pthread_mutexattr_init(&attr); 
// 		// 设置 recursive 属性
// 		pthread_mutexattr_settype(&attr,PTHREAD_MUTEX_RECURSIVE_NP); 
//		pthread_mutex_init(&_mutex, &attr);
//		pthread_mutexattr_destroy(&attr);
		pthread_mutex_init(&m_tThreadMutex, NULL);
#endif
	}
	~CThreadMutex()
	{  
#ifdef WIN32
		DeleteCriticalSection(&m_tThreadMutex);
#else
		pthread_mutex_destroy(&m_tThreadMutex);
#endif
	}	
	void Lock()
	{
#if defined(WIN32)
		EnterCriticalSection(&m_tThreadMutex);
#else
		if( pthread_mutex_lock(&m_tThreadMutex) != 0 )
			printf("Error:	Lock fail!\n");
#endif
	}	
	void UnLock()
	{
#if defined(WIN32)
		LeaveCriticalSection(&m_tThreadMutex);
#else
		if ( pthread_mutex_unlock(&m_tThreadMutex) != 0 )
			printf("Error:	Unlock fail!\n");
#endif
	}
#endif

private:
#ifdef __USE_THREAD__
#if defined(WIN32)
	CRITICAL_SECTION	m_tThreadMutex;
#else
	pthread_mutex_t		m_tThreadMutex;
#endif
#endif
};

template <class T>
class CSingleton
{
public:
    static T* GetInstance();

protected:
    CSingleton()
    {
    }
    virtual ~CSingleton()
    {
    }

protected:    
    //friend class auto_ptr<CSingleton>;
    static auto_ptr<T> m_pInstance;
    static CThreadMutex m_tThreadMutex;
};

template <class T>
auto_ptr<T> CSingleton<T>::m_pInstance;

template <class T>
CThreadMutex CSingleton<T>::m_tThreadMutex;

template <class T>
inline T* CSingleton<T>::GetInstance()
{
	//Double-Checked Locking
    if (0 == m_pInstance.get())
    {
        THREAD_LOCK(m_tThreadMutex);
        if (0 == m_pInstance.get())
        {
            m_pInstance.reset(::new T);
        }
        THREAD_UNLOCK(m_tThreadMutex);
    }

    return m_pInstance.get();
}

#endif /* __SINGLETON_H__ */

//demo
/*
class CTestSingleton
: public CSingleton<CTestSingleton>
{
public:
	
    void Set(int a)
    {
        m_a = a;
    }
    int Get()
    {
        return m_a;
    }
	
private:
    CTestSingleton()
        : m_a(0)
    {
		
    }
	//子类中需要增加这句话
    DECLARE_SINGLETON_CLASS(CTestSingleton)
private:
    int m_a;
};


int main()
{
    if (NULL == CTestSingleton::GetInstance())
    {
        cout << "GetInstance() error!" << endl;
    }
	
    cout << "before set: " << CTestSingleton::GetInstance()->Get() << endl;
	
    CTestSingleton::GetInstance()->Set(1002222);
	
    cout << "after set: " << CTestSingleton::GetInstance()->Get() << endl;
	
    return 0;
}
*/



