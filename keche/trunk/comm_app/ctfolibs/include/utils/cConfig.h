
/***********************************************************************
 ** Copyright (c)2010
 ** All rights reserved.
 ** 
 ** File name  : cConfig.h
 ** Author     : lizp (lizp.net@gmail.com)
 ** Date       : 2009-11-3 ÏÂÎç 3:42:38
 ** Comments   : ...
 ***********************************************************************/

#ifndef _HEAD_CCONFIG_H
#define _HEAD_CCONFIG_H

#include <stdarg.h>
#include <stdio.h>

#define TYPE_SECTION        1
#define TYPE_TOKEN_SCALAR   2
#define TYPE_TOKEN_VECTOR   3
#define TYPE_COMMENT        4
#define TYPE_BLANK      	5

#define fIllegalType(type) (((type) < TYPE_SECTION) || ((type) > TYPE_BLANK))
#define fIsToken(type) \
    (((type) == TYPE_TOKEN_SCALAR) || ((type) == TYPE_TOKEN_VECTOR))

struct stLINE;

typedef struct stTOKEN {
    int              type;
    int              index;
    char            *name;
    char            *value;
    struct stTOKEN  *next;      // point to the next token in this line
    struct stTOKEN  *snext;     // point to the next token within a
                    // section. This pointer is valid only
                    // when type is TYPE_TOKEN_*.
    struct stLINE   *line;      // point to the line which contains
                    // this token.
} stToken;

typedef struct stLINE {
    struct stTOKEN  *token;     // point to the first token in this line
    struct stTOKEN  *etoken;    // point to the first TYPE_TOKEN_*
                    // token within a section. This pointer
                    // is valid only when this line is the
                    // definition of a section.
    struct stLINE   *prev;      // previous line
    struct stLINE   *next;      // next line
} stLine;

/*
 * If functions declared within this class return an pointer and no further
 * explanation, then the return value of NULL pointer indicates error,
 * others indicate success. If the return value has the type of integer and no
 * further explanation, zero indicates success while -1 indicates error.
 */

class CCConfig {
private:
    char       filename[128];
    FILE      *fp;

    stLine    *lines;
    stLine   **sections;

    int       iTotalSections;

    // If configuration was changed by function call like 'fSetValue' since
    // the loading at very beginning, 'iDirtyFlag' will be set to 1. When
    // the instance is deleted, changes will be saved to file.
    // But if iWriteNow flag is set, your modification will be written
    // to file immediately.
    int       iDirtyFlag;
    int       iWriteNow;

    int       iTokensPerLine;

    int       iCaseSensitive;

public:
    // This flag indicates whether the instance is properly initialized.
    int       iIAmReady;

public:
    CCConfig(const char *fname);
    ~CCConfig();

    int  fReload(const char *path = NULL);
    int  fLoadFromFile(const char *path);
    int  fIAmReady(void);
    int  fSetWriteNow(int writenow = 1);
    int  fSetCaseSensitive(int yes = 1);
    int  fSetColumn(int column = 0);
    int  fDump(const char *path);
    int  fSaveMeToFile(void);
    void fReleaseMemory(void);

    /*
     * This member function will get the value according to the id. The
     * value will be copied to memory pointed by parameter 'value'. So
     * be careful about the 'value' parameter.
     *
     * The parameter 'id' is also used to be a flag to distinguish the
     * type of configuration item you request. If it is NULL, I consider
     * that what you want is TYPE_TOKEN_VECTOR. This kind of token is
     * defined like the example below.
     *  [colors]
     *  red orange yellow green cyan blue purple
     * While non-NULL pointer indicates TYPE_TOKEN_SCALAR, for an example,
     *  [server configuration]
     *  ip = 127.0.0.1
     *  port = 6001
     *
     *  [clients]
     *  client = 192.168.1.11
     *  client = 132.108.58.51
     * Under this kind of circumstance, 'id' may be a pointer to a
     * character string "ip", "port" or "clients".
     *
     * So if you want to retrieve the ip address from the above example,
     * you may issue a function call like this,
     *  char  caIpAddr[16];
     *  config->fGetValue("server configuration", "ip", caIpAddr);
     * and if you want to retrieve the second client address, you may issue
     * the function call like this,
     *  char  caIpAddr[16];
     *  config->fGetValue("clients", "client", caIpAddr, 2);
     *
     * If you want to retrieve the yellow color from colors section in the
     * above example, be careful, it's type is different from "ip" or
     * "client", you may issue a function call like this,
     *  char   caColor[16];
     *  config->fGetValue("colors", NULL, caColor, 3);
     */
    int fGetValue(const char *section, const char *id, char *value, int index = 1);
    int fGetValue(const char *section, const char *id, const char *defval, char *value, int index = 1);
    int fGetValue(const char *section, const char *id, int defval, int &value, int index = 1);
    int fGetValue(const char *section, const char *id, unsigned char defval, unsigned char &value, int index = 1);
    int fGetValue(const char *section, const char *id, float defval, float &value, int index = 1);

    /*
     * Compared with fGetValue, fSetValue sets the value of id. The grammar
     * is almost the same with fGetValue. Now let's reuse the example above
     * to show this. Function call
     *  config->fSetValue("server configuration", "ip", "192.168.1.11");
     * sets the ip address to 192.168.1.11 in server configuration section,
     * while function call
     *  config->fSetValue("colors", NULL, "black", 3);
     * sets the third item in colors section, that is yellow, to black
     * color.
     * And function call
     *  config->fSetValue("clients", "client", "203.93.112.1", 2);
     * sets the second client's address from 132.108.58.51 to 203.93.112.1.
     */
    int fSetValue(const char *section, const char *id, const char *value, int index = 1);
    int fSetValue(const char *section, const char *id, int value, int index = 1);
    int fSetValue(const char *section, const char *id, float value, int index = 1);
    int fSetValue1(const char *section, const char *id, const char *value, int index = 1);

    /*
     * This function returns the number of 'id's defined in 'section'.
     */
    int fGetCount(const char *section, const char *id = NULL);

    /*
     * This function returns index of the first apperance of 'value' in
     * 'section'. If 'value' is NULL, the return value indicates which
     * section the 'section' is.
     */
    int fGetIndex(const char *section, const char *id = NULL, int start = 0);

    /*
     * Function fAddToken adds a section or a token exactly after the item
     * which has the corresponding type and the index number equals to
     * 'pos'. The newly added item will be the first one by default.
     */
    int fAddToken(const char *section, int where = 0, const char *id = NULL, const char *value = NULL);

    /*
     * Function fAddComment adds comment line to configuration file. If
     * both section and id are all NULL pointer, comment will be added
     * after the 'where' line. If both section and id are all non-NULL
     * pointer, comment will be added before the 'where'th definition
     * of 'id' in 'section'. If section isn't NULL and id is NULL, comment
     * will be added before the definition of 'section'. All other
     * combinations of parameters will cause failure.
     * By the way, a blank line other than comment line will be added
     * if parameter comment is NULL pointer.
     */
    int fAddComment(const char *comment, int where = 0, const char *section = NULL, const char *id = NULL);

    /*
     * If id is a NULL pointer, fDelToken deletes a TYPE_TOKEN_VECTOR token
     * with index from the specified section. If id isn't NULL, fDelToken
     * deletes the 'id = value' pattern found in the specified section.
     */
    int fDelToken(const char *section, const char *id = NULL, int index = 1);

    /*
     * To prevent you from deleting a whole section accidently, it's
     * defined as a separate function.
     */
    int fDelSection(const char *section);

    int fExistSection(const char *section);

    /*
     * Do you really need comments about this function here?
     */
    int fTestMe(void);
};

#endif  // #ifndef _HEAD_CCONFIG_H


