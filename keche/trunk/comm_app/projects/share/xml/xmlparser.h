/*
 * xmlPaser.h
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */

#ifndef __XMLPASER_H_
#define __XMLPASER_H_

#include <vector>
#include <map>
#include <qstring.h>

class TiXmlNode ;
class CXmlParser
{
	class CValue
	{
	public:
		CValue(){};
		~CValue(){};
		// 取得参数总个数
		int GetCount( void ) ;
		// 取得当前值
		const char * GetValue( int index ) ;
		// 重载取得当前值
		const char* operator[] ( int index ) ;
		// 添加元素
		void AddValue( const char *data ) ;
	private:
		// 添加对象的BUF
		typedef std::vector<CQString>  CVectorValue;
		// 存放XML值数据
		CVectorValue _vecValue ;
	};
public:
	CXmlParser() ;
	~CXmlParser() ;

	// 取得XML的值
	bool LoadXml( const char* xmlText ) ;
	// 取得键值个数
	const int GetCount( const char *key ) ;
	// 取得字符串值
	const char * GetString( const char *key, const int index ) ;
	// 取得BOOL形的变量
	const bool GetBoolean( const char *key, const int index ) ;
	// 取得整形变量值
	const int  GetInteger( const char *key, const int index ) ;

private:
	// 解析XML数据
	void ParseXml( TiXmlNode* pParent, const char *szName , bool bFirst ) ;
	// 解析XML的属性
	void ParserAttr( TiXmlNode *pNode, const char *szName ) ;
	// 添加到解析数对象中
	void AddMapKey( const char *key, const char *val ) ;

private:
	// 对应key的值
	typedef std::map<CQString,CValue*>  CMapKey2Value ;
	// 对应的map值
	CMapKey2Value  _mapValue ;
};

/*Xml 封装的解析类*/
class TiXmlDocument;
class TiXmlElement;
class CXmlBuilder
{
public:
	CXmlBuilder(const char* pRootName,const char* pChildName,const char* pItemName);
	virtual ~CXmlBuilder();

	void SetRootAttribute(const char* pName,const char* pValue);
	void SetChildAttribute(const char* pName,const char* pValue);
	void SetItemAttribute(const char* pName,const char* pValue);

	void InsertItem();
	void GetXmlText(CQString &sXmlText);

private:
	 TiXmlDocument *_pDoc;
	 TiXmlElement  *_pRoot;
	 TiXmlElement  *_pChild;
	 TiXmlElement  *_pItem;
};


#endif /* XMLPASER_H_ */
