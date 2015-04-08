/*
 * xmlParser.cpp
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */

#include <tinyxml.h>
#include "xmlparser.h"
#include <tools.h>

#ifndef _WIN32
#include <strings.h>
#define stricmp  strcasecmp
#endif
/////////////////////// CValue 处理 /////////////////////////
// 取得值个数
int CXmlParser::CValue::GetCount( void )
{
	return (int) _vecValue.size() ;
}
// 取得对应值
const char * CXmlParser::CValue::GetValue( int index )
{
	return (const char*) _vecValue[index] ;
}

// 实现中括号的操作
const char* CXmlParser::CValue::operator[] ( int index )
{
	return ( const char *) _vecValue[index] ;
}

// 添加数据
void CXmlParser::CValue::AddValue( const char *data )
{
	_vecValue.push_back( data ) ;
}

////////////////////////// CXmlConfig /////////////////////////////////
CXmlParser::CXmlParser()
{

}

CXmlParser::~CXmlParser()
{
	if ( _mapValue.empty() ){
		return ;
	}

	CMapKey2Value::iterator it  ;
	for ( it = _mapValue.begin(); it != _mapValue.end(); ++ it ){
		delete it->second ;
	}
	_mapValue.clear() ;
}

// 取得XML的值
bool CXmlParser::LoadXml( const char* xmlText )
{
	TiXmlDocument doc ;
	doc.Parse( xmlText ) ;

	TiXmlElement *root = doc.RootElement() ;
	if( root == NULL ) {
		return false ;
	}

	ParseXml( root , root->Value() , true ) ;

	return true ;
}

// 取得键值个数
const int CXmlParser::GetCount( const char *key )
{
	CMapKey2Value::iterator it = _mapValue.find( key ) ;
	if ( it == _mapValue.end() ){
		return 0 ;
	}
	CValue *pValue = it->second ;
	return pValue->GetCount() ;
}

// 取得字符串值
const char * CXmlParser::GetString( const char *key, const int index )
{
	CMapKey2Value::iterator it = _mapValue.find( key ) ;
	if ( it == _mapValue.end() ){
		return NULL ;
	}
	CValue *pValue = it->second ;
	return pValue->GetValue( index ) ;
}

// 取得BOOL形的变量
const bool CXmlParser::GetBoolean( const char *key, const int index )
{
	CMapKey2Value::iterator it = _mapValue.find( key ) ;
	if ( it == _mapValue.end() ){
		return false ;
	}
	CValue *pValue = it->second ;
	const char *ptr = pValue->GetValue(index) ;
	if ( ptr == NULL )
		return false ;

	if ( stricmp(ptr , "true" ) ==  0 ){
		return true ;
	}
	return false ;
}

// 取得整形变量值
const int  CXmlParser::GetInteger( const char *key, const int index )
{
	CMapKey2Value::iterator it = _mapValue.find( key ) ;
	if ( it == _mapValue.end() ){
		return -1 ;
	}

	CValue *pValue = it->second ;
	const char *ptr = pValue->GetValue(index) ;
	if ( ptr == NULL )
		return -1 ;

	return atoi( ptr ) ;
}

// 解析XML属性
void CXmlParser::ParserAttr( TiXmlNode *pNode, const char *szName )
{
	TiXmlAttribute* attr = pNode->ToElement()->FirstAttribute();
	if(attr){

		TiXmlNode* node = pNode;
		while(node){
			while(attr){
				CQString  temp = szName ;
				temp += ":" ;
				temp += attr->Name() ;

				AddMapKey( temp.GetBuffer() , attr->Value() ) ;

				attr = attr->Next();
			}
			node =  node->NextSiblingElement();
		}
	}
}

// 解析XML元素
void CXmlParser::ParseXml( TiXmlNode* pParent, const char *szName , bool bFirst )
{
	if(pParent == NULL) return;

	CQString s ;
	if ( szName != NULL ){
		s = szName ;
	}

	if ( bFirst && szName ) {
		ParserAttr( pParent , szName ) ;
	}

	TiXmlNode* pchild = pParent->FirstChild();
	while(pchild){
		int t = pchild->Type();
		if( t == TiXmlNode::TINYXML_ELEMENT ){
			CQString temp = s ;
			if ( ! temp.IsEmpty() ){
				temp += "::" ;
			}
			temp += pchild->Value() ;

			ParserAttr( pchild, temp.GetBuffer() ) ;
			ParseXml( pchild, (char *)temp , false );

		} else if( t == TiXmlNode::TINYXML_TEXT ) {
			AddMapKey( s.GetBuffer() , pchild->Value() ) ;
		}
		pchild = pchild->NextSibling();
	}
}

// UTF-8转成本地编码
static bool utf82locale( const char *szdata, const int nlen , CQString &out )
{
	int   len = nlen + 1024 ;
	char *buf = new char[ len ] ;
	memset( buf, 0 , len ) ;

	if( u2g( (char *)szdata , nlen , buf, len ) == -1 ){
		delete [] buf ;
		return false ;
	}
	buf[len] = 0 ;
	out.SetString( buf ) ;
	delete [] buf ;

	return true ;
}

// 添加映射对象中
void CXmlParser::AddMapKey( const char *key, const char *val )
{
	if ( key == NULL || val == NULL )
		return ;

	CValue *pValue = NULL ;
	CMapKey2Value::iterator it = _mapValue.find( key ) ;
	if ( it != _mapValue.end() ) {
		pValue = it->second ;
	}else{
		pValue = new CValue ;
		_mapValue.insert( std::pair<CQString, CValue*>( key, pValue ) ) ;
	}

	// 处理UTF-8的数据
	CQString stemp ;
	utf82locale( val, strlen(val) , stemp ) ;
	pValue->AddValue( stemp.GetBuffer() ) ;
}

//========================================================================

#define XML_HEADER  "<?xml version='1.0' encoding='utf-8' ?>"
//----------------------------------------------------------------------------
CXmlBuilder::CXmlBuilder(const char* pRootName,const char* pChildName,const char* pItemName)
{
	_pDoc   = new TiXmlDocument ;
	_pRoot  = new TiXmlElement(pRootName);
	_pChild = new TiXmlElement(pChildName);
	_pItem  = new TiXmlElement(pItemName);
	_pDoc->Parse( XML_HEADER ) ;
}
//----------------------------------------------------------------------------
CXmlBuilder::~CXmlBuilder()
{
	if(_pRoot)
		delete _pRoot;
	if(_pChild)
		delete _pChild;
	if(_pItem)
		delete _pItem;
	if ( _pDoc )
		delete _pDoc ;
}
//----------------------------------------------------------------------------
void CXmlBuilder::SetRootAttribute(const char* pName,const char* pValue)
{
	_pRoot->SetAttribute(pName,pValue);
}
//----------------------------------------------------------------------------
void CXmlBuilder::SetChildAttribute(const char* pName,const char* pValue)
{
	_pChild->SetAttribute(pName,pValue);
}
//----------------------------------------------------------------------------
void CXmlBuilder::SetItemAttribute(const char* pName,const char* pValue)
{
	_pItem->SetAttribute(pName,pValue);
}
//----------------------------------------------------------------------------
void CXmlBuilder::InsertItem()
{
	_pChild->InsertEndChild(*_pItem);
}
//----------------------------------------------------------------------------
void CXmlBuilder::GetXmlText(CQString &sXmlText)
{
	TiXmlPrinter  Printer;
	_pRoot->InsertEndChild(*_pChild);
	_pDoc->InsertEndChild(*_pRoot);
	_pDoc->Accept(&Printer);
	sXmlText.SetString( Printer.CStr() ) ;
}
