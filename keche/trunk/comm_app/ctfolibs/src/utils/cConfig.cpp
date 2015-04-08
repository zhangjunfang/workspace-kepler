
#include <stdlib.h>
#ifndef _NI_WIN_
#include <unistd.h>
#endif
#include <stdarg.h>
#include <string.h>
#include <errno.h>

//#include "comlog.h"
#include "cConfig.h"

#define STRCMP(A, B) (iCaseSensitive ? strcmp((A), (B)) : strcasecmp((A), (B)))

extern int errno;

CCConfig::CCConfig(const char *fname)
{
    char caBakFilename[148];

    iIAmReady      = 0;
    lines          = NULL;
    sections       = NULL;
    iDirtyFlag     = 0;
    iWriteNow      = 1;
    iCaseSensitive = 1;
    iTotalSections = 0;
    iTokensPerLine = 0;
    
    memset(filename, 0, sizeof(filename));

    if (fname == NULL)
    {
//        ERRLOG(NULL, 0, "NULL parameter.");
        return;
    }

    if (strlen(fname) >= sizeof(filename))
    {
//        ERRLOG(NULL, 0, "Parameter \"%s\" too long.", fname);
        return;
    }
    
    strcpy(filename, fname);

    if (fLoadFromFile(filename) ==  - 1)
    {
//        ERRLOG(NULL, 0, "Load from file \"%s\" fail.", fname);
        return;
    }

    iIAmReady = 1;

    memset(caBakFilename, 0, sizeof(caBakFilename));
    sprintf(caBakFilename, "%s.lfbak", filename);
    
    fDump(caBakFilename);
}

CCConfig::~CCConfig()
{
    if (iIAmReady && iDirtyFlag && (fSaveMeToFile() ==  - 1))
    {
//        ERRLOG(NULL, 0, "CCConfig destructor: dump to file fail.");
    }

    fReleaseMemory();
}

int CCConfig::fIAmReady(void)
{
    return (iIAmReady);
}

int CCConfig::fSetWriteNow(int writenow)
{
    int oldflag = iWriteNow;

    iWriteNow = (writenow ? 1 : 0);

    return (oldflag);
}

int CCConfig::fSetCaseSensitive(int yes)
{
    int oldflag = iCaseSensitive;

    iCaseSensitive = (yes ? 1 : 0);

    return (oldflag);
}

int CCConfig::fSetColumn(int column)
{
    int oldcolumn = iTokensPerLine;

    iTokensPerLine = (column > 0 ? column : 0);

    return (oldcolumn);
}

int CCConfig::fReload(const char *path)
{
    fReleaseMemory();

    iIAmReady = 0;
    lines = NULL;
    sections = NULL;
    iDirtyFlag = 0;
    iWriteNow = 1;
    iCaseSensitive = 1;
    iTotalSections = 0;

    if (fLoadFromFile((path == NULL) ? filename : path) ==  - 1)
    {
//        ERRLOG(NULL, 0, "Load from file \"%s\" fail.", filename);
        return (-1);
    }

    iIAmReady = 1;
    
    return (0);
}

int CCConfig::fSaveMeToFile(void)
{
    char caTempFilename[148];

    if (!iIAmReady)
    {
        return (-1);
    }

    if (filename[0] == '\0')
    {
        return (-1);
    }

    memset(caTempFilename, 0, sizeof(caTempFilename));
    sprintf(caTempFilename, "%s.lftmp", filename);
    
    if (fDump(caTempFilename) ==  - 1)
    {
//        ERRLOG(NULL, 0, "Dump to file \"%s\" fail.", caTempFilename);
        return (-1);
    }
    
    //LOG1(NULL, 0, "fDump(%s) ok.", caTempFilename);

    if (unlink(filename) ==  - 1)
    {
//        ERRLOG(NULL, 0, "Can't delete %s, errno=%d. Check %s for new configuration.",
//                filename, errno, caTempFilename);
        return (-1);
    }
    
    //LOG1(NULL, 0, "unlink(%s) ok.", filename);

    if (rename(caTempFilename, filename) ==  - 1)
    {
//        ERRLOG(NULL, 0, "rename %s to %s fail, errno=%d. Check %s for new configuration.",
//            caTempFilename, filename, errno, caTempFilename);
        return (-1);
    }
    
    //LOG2(NULL, 0, "rename %s to %s ok.", caTempFilename, filename);

    return (0);
}

#define fIsDelimiter(c) (((c) == ' ') || ((c) == '\t'))

#define STH_WRONG_EXIT(msg) { \
    fclose(fp); \
    return(-1); \
}

#define ADD_LINE \
{ \
    if ((line = new stLine) == NULL) \
    { \
        STH_WRONG_EXIT("memory allocation fail"); \
    } \
\
    line->next   = NULL; \
    line->prev   = NULL; \
    line->token  = NULL; \
    line->etoken = NULL; \
\
    if (curline == NULL) \
    { \
        lines = curline = line; \
    } \
    else \
    { \
        curline->next = line; \
        line->prev    = curline; \
        curline       = line; \
    } \
\
    line     = NULL; \
    curtoken = NULL; \
}

#define ADD_TOKEN(Type, Index, Name, Namelen, Value, Valuelen) \
{ \
    if ((token = new stToken) == NULL) \
        STH_WRONG_EXIT("memory allocation fail"); \
\
    token->type  = (Type); \
    token->index = (Index); \
\
    if (!(Name)) \
    { \
        token->name = NULL; \
    } \
    else \
    { \
        if((Namelen) <= 0) \
            STH_WRONG_EXIT("Illegal name"); \
\
        if((token->name  = new char[(Namelen)+1]) == NULL) \
            STH_WRONG_EXIT("memory allocation fail"); \
\
        memcpy(token->name, (Name), (Namelen)); \
        token->name[(Namelen)] = '\0'; \
    } \
\
    if(!(Value)) \
    { \
        token->value = NULL; \
    } \
    else \
    { \
        if((Valuelen) <= 0) \
            STH_WRONG_EXIT("Illegal value"); \
\
        if((token->value  = new char[(Valuelen)+1]) == NULL) \
            STH_WRONG_EXIT("memory allocation fail"); \
\
        memcpy(token->value, (Value), (Valuelen)); \
        token->value[(Valuelen)] = '\0'; \
    } \
\
    token->next  = NULL; \
    token->snext = NULL; \
    token->line  = NULL; \
\
    if(curtoken  == NULL) \
    { \
        curline->token = curtoken = token; \
    } \
    else \
    { \
        curtoken->next = token; \
        curtoken       = token; \
    } \
\
    token = NULL; \
}

int CCConfig::fLoadFromFile(const char *path)
{
    char caLine[1024], caTempLine[1024], caTempLine2[1024];
    char *pcEqualSign;
    int iEqualSignPos, iSectionDefined;
    int iTokenScalarDefined, iTokenVectorDefined;
    int i, j, k, l, m, flag, iStart, iEnd, iTokenEnd, iIndex;
    stToken *token,  *curtoken,  *ptoken,  *ntoken;
    stLine *line,  *curline;

    if ((fp = fopen(path, "r")) == NULL)
    {
        if ((fp = fopen(path, "w+")) == NULL)
        {
//            ERRLOG(NULL, 0, "Create %s fail, errno=[%d].", path, errno);
            return (-1);
        }
        
        fclose(fp);
        
        if ((fp = fopen(path, "r")) == NULL)
        {
//            ERRLOG(NULL, 0, "reopen %s fail, errno=[%d].", path, errno);
            return (-1);
        }
    }

    curline = lines;
    iSectionDefined = 0;
    iTokenScalarDefined = iTokenVectorDefined = 1;
    iIndex = 1;
    
    while (!feof(fp))
    {
        memset(caLine, 0, sizeof(caLine));
        
        if (fgets(caLine, sizeof(caLine), fp) == NULL)
        {
            continue;
        }

        iStart = 0;
        
        if ((iEnd = (strlen(caLine) - 1)) < 0)
        {
            STH_WRONG_EXIT("get line fail");
        }
        
		//判断末尾是否是换行，这一点尤其注意，windows下是回车换行，所以不能在win下编辑该配置文件
        if (caLine[iEnd] == '\n')
        {
			if ( caLine[iEnd-1] == '\r' )
				iEnd --;	
            caLine[iEnd--] = '\0';
        }

		if (caLine[iEnd] == '\r')
        {
            caLine[iEnd--] = '\0';
        }
        
        while ((iEnd >= iStart) && fIsDelimiter(caLine[iEnd]))
        {
            iEnd--;
        }

        ADD_LINE;
        //LOG1(NULL, 0, "dump new line: \"%s\"", caLine);

        // Is it a blank line?
        if (iStart > iEnd)
        {
            //LOG(NULL, 0, "TYPE_BLANK");
            ADD_TOKEN(TYPE_BLANK, 0, NULL, 0, NULL, 0);
            continue;
        }

        // Check whether the line is comment.
        if (caLine[0] == '#')
        {
            ADD_TOKEN(TYPE_COMMENT, 0, caLine, iEnd + 1, NULL, 0);
            continue;
        }

        while ((iStart <= iEnd) && fIsDelimiter(caLine[iStart]))
        {
            iStart++;
        }

        // Check whether the line is the definition of section.
        if ((caLine[iStart] == '[') && (caLine[iEnd] == ']'))
        {
            i = iStart + 1;
            j = iEnd - 1;
            
            while ((i <= iEnd) && fIsDelimiter(caLine[i]))
            {
                i++;
            }
            
            while ((j >= iStart) && fIsDelimiter(caLine[j]))
            {
                j--;
            }
            
            for (flag = 0, l = 0, k = i; k <= j; k++)
            {
                if (!fIsDelimiter(caLine[k]))
                {
                    flag = 0;
                    caTempLine[l++] = caLine[k];
                }
                else
                {
                    if (flag == 0)
                    {
                        caTempLine[l++] = ' ';
                        flag = 1;
                    }
                }
            }
            
            ADD_TOKEN(TYPE_SECTION, 0, caTempLine, l, NULL, 0);
            
            //ADD_TOKEN(TYPE_SECTION, 0, &caLine[i], j-i+1, NULL, 0);
            
            if (!iTokenScalarDefined && !iTokenVectorDefined)
            {
            }
            
            iIndex = iSectionDefined = 1;
            iTokenScalarDefined = iTokenVectorDefined = 0;
            
            continue;
        }

        if (iSectionDefined != 1)
        {
            STH_WRONG_EXIT("define section first");
        }

        pcEqualSign = strchr(caLine, '=');
        
        if (pcEqualSign == NULL)
        {
            // no equal sign found. So this line should likes this:
            //  [idenfifier[ \t]*]*
            if (iTokenScalarDefined == 1)
            {
                STH_WRONG_EXIT("Cannot mix type scalar and vector together");
            }
            

            while (iStart <= iEnd)
            {
                iTokenEnd = iStart;
                
                while ((iTokenEnd <= iEnd) && !fIsDelimiter(caLine[iTokenEnd]))
                {
                    iTokenEnd++;
                }
                
                ADD_TOKEN(TYPE_TOKEN_VECTOR, iIndex, NULL, 0, &caLine[iStart], iTokenEnd - iStart);
                iIndex++;
                
                while ((iTokenEnd <= iEnd) && fIsDelimiter(caLine[iTokenEnd]))
                {
                    iTokenEnd++;
                }
                
                iStart = iTokenEnd;
            }

            // If everything is ok, continue to process the next
            // line.
            iTokenVectorDefined = 1;
            
            continue;
        }

        if (iTokenVectorDefined == 1)
        {
            STH_WRONG_EXIT("Cannot mix type scalar and vector together");
        }


        iEqualSignPos = pcEqualSign - caLine;
        pcEqualSign = strrchr(caLine, '=');
        
        if (pcEqualSign == NULL)
        {
            STH_WRONG_EXIT("I don't know why this situation occur");
        }
        
        if ((pcEqualSign - caLine) != iEqualSignPos)
        {
            STH_WRONG_EXIT("Two equal sign in one line");
        }
        
        i = iEqualSignPos - 1;
        j = iEqualSignPos + 1;
        
        while ((i >= iStart) && fIsDelimiter(caLine[i]))
        {
            i--;
        }
        
        while ((j <= iEnd) && fIsDelimiter(caLine[j]))
        {
            j++;
        }
        
        for (flag = 0, l = 0, k = iStart; k <= i; k++)
        {
            if (!fIsDelimiter(caLine[k]))
            {
                flag = 0;
                caTempLine[l++] = caLine[k];
            }
            else
            {
                if (flag == 0)
                {
                    caTempLine[l++] = ' ';
                    flag = 1;
                }
            }
        }
        
        for (flag = 0, m = 0, k = j; k <= iEnd; k++)
        {
            if (!fIsDelimiter(caLine[k]))
            {
                flag = 0;
                caTempLine2[m++] = caLine[k];
            }
            else
            {
                if (flag == 0)
                {
                    caTempLine2[m++] = ' ';
                    flag = 1;
                }
            }
        }
        
        ADD_TOKEN(TYPE_TOKEN_SCALAR, 1, caTempLine, l, caTempLine2, m);
        //ADD_TOKEN(TYPE_TOKEN_SCALAR, 1, &caLine[iStart], i-iStart+1, &caLine[j], iEnd-j+1);
        iTokenScalarDefined = 1;
    }
    
    fclose(fp);

    // Let each token within a line has a pointer to the line.
    for (curline = lines; curline != NULL; curline = curline->next)
    {
        curtoken = curline->token;
        
        while (curtoken != NULL)
        {
            curtoken->line = curline;
            curtoken = curtoken->next;
        }
    }

    // count the number of sections, and save the respective line pointer
    // in variable 'sections'.
    for (i = 0, curline = lines; curline != NULL; curline = curline->next)
    {
        token = curline->token;
        
        if ((token != NULL) && (token->type == TYPE_SECTION))
        {
            i++;
        }
    }
    
    iTotalSections = i;
    
    sections = new stLine *[iTotalSections + 1];
    
    if (sections == NULL)
    {
//        ERRLOG(NULL, 0, "memory allocation fail.");
        return (-1);
    }
    
    for (j = 0, curline = lines; curline != NULL; curline = curline->next)
    {
        token = curline->token;
        
        if ((token != NULL) && (token->type == TYPE_SECTION))
        {
            *(sections + j) = curline;
            j++;
        }
    }
    
    *(sections + j) = NULL;
    
    for (j = 1; j < iTotalSections; j++)
    {
        for (k = 0; k < j; k++)
        {
            if (!STRCMP((*(sections + k))->token->name, (*(sections + j))->token->name))
            {
//                ERRLOG(NULL, 0, "Duplicate sections: [%s]", sections[j]->token->name);
                return (-1);
                break;
            }
        }
    }

    // now set each section's etoken pointer.
    for (i = 0; i < iTotalSections; i++)
    {
        curline = *(sections + i);
        
        while ((curline != NULL) && (curline != *(sections + i + 1)))
        {
            token = curline->token;
            
            if ((token == NULL) || !fIsToken(token->type))
            {
                curline = curline->next;
                continue;
            }
            
            (*(sections + i))->etoken = token;
            
            break;
        }
    }

    // now link the tokens within a section together.
    for (i = 0; i < iTotalSections; i++)
    {
        curline = *(sections + i);
        j = 1;
        
        while ((curline != NULL) && (curline != *(sections + i + 1)))
        {
            token = curline->token;
            
            if ((token == NULL) || !fIsToken(token->type))
            {
                curline = curline->next;
                continue;
            }
            
            if (j == 1)
            {
                ptoken = token;
                j = 0;
            }
            else
            {
                ptoken->snext = token;
                ptoken = token;
            }

            while (token->next != NULL)
            {
                token = token->next;
                ptoken->snext = token;
                ptoken = token;
            }
            
            curline = curline->next;
        }
    }

    // now compute the index parameter of each TYPE_TOKEN_SCALAR token.
    for (i = 0, line =  *sections; line != NULL; line = *(sections + (++i)))
    {
        token = (*(sections + i))->etoken;
        
        if ((token == NULL) || (token->type != TYPE_TOKEN_SCALAR))
        {
            continue;
        }
        
        while (token != NULL)
        {
            ntoken = token->snext;
            
            while (ntoken != NULL)
            {
                if (!STRCMP(token->name, ntoken->name))
                {
                    ntoken->index++;
                }
                
                ntoken = ntoken->snext;
            }
            
            token = token->snext;
        }
    }

    return (0);
}

int CCConfig::fDump(const char *path)
{
    FILE *fpDump;
    stLine *l;
    stToken *t;
    int iMaxTokenLen, iTokens, len, i;

    if (!iIAmReady)
    {
        return (-1);
    }

    if ((fpDump = fopen(path, "w")) == NULL)
    {
//        ERRLOG(NULL, 0, "open file %s fail.", path);
        return (-1);
    }

    for (l = lines; l != NULL; l = l->next)
    {
        if ((t = l->token) == NULL)
        {
            continue;
        }
        
        if (t->type == TYPE_TOKEN_VECTOR)
        {
            for (iMaxTokenLen = 0; t != NULL; t = t->next)
            {
                len = strlen(t->value);
                if (iMaxTokenLen < len)
                {
                    iMaxTokenLen = len;
                }
            }
            
            iMaxTokenLen++;
            
            if (iTokensPerLine > 0)
            {
                iTokens = iTokensPerLine;
            }
            else
            {
                iTokens = 70 / iMaxTokenLen;
                
                if (iTokens > 16)
                {
                    iTokens = 16;
                }
                
                if (iTokens < 1)
                {
                    iTokens = 1;
                }
            }
            
            for (t = l->token, i = 0; t != NULL; t = t->next)
            {
                fprintf(fpDump, "%-*s", iMaxTokenLen, t->value);
                if (!(++i % iTokens))
                {
                    fprintf(fpDump, "\n");
                    i = 0;
                }
            }
            
            if (i)
            {
                fprintf(fpDump, "\n");
            }
            
            continue;
        }
        else if (t->type == TYPE_COMMENT)
        {
            fprintf(fpDump, "%s\n", t->name);
        }
        else if (t->type == TYPE_BLANK)
        {
            fprintf(fpDump, "\n");
        }
        else if (t->type == TYPE_SECTION)
        {
            fprintf(fpDump, "[%s]\n", t->name);
        }
        else if (t->type == TYPE_TOKEN_SCALAR)
        {
            fprintf(fpDump, "%-15s = %s\n", t->name, t->value);
        }
        else
        {
//            ERRLOG(NULL, 0, "fDump: unknown type %d.", t->type);
        }
    }

    fclose(fpDump);
    iDirtyFlag = 0;

    return (0);
}

void CCConfig::fReleaseMemory(void)
{
    stLine *curline,  *nextline;
    stToken *curtoken,  *nexttoken;

    curline = lines;
    
    while (curline != NULL)
    {
        nextline = curline->next;
        curtoken = curline->token;
        
        while (curtoken != NULL)
        {
            nexttoken = curtoken->next;
            delete curtoken->name;
            delete curtoken->value;
            delete curtoken;
            curtoken = nexttoken;
        }
        
        delete curline;
        
        curline = nextline;
    }
    
    lines = NULL;

    if (sections != NULL)
    {
        delete []sections;
    }
}

int CCConfig::fGetValue(const char *section, const char *id, char *value, int index)
{
    stLine *line;
    stToken *token;
    int i;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if (section == NULL)
    {
        return (-1);
    }
    
    for (i = 0, line =  *sections; line != NULL; line = *(sections + (++i)))
    {
        if (!STRCMP(section, line->token->name))
        {
            break;
        }
    }
    
    if (line == NULL)
    {
        return (-1);
    }
    
    if ((token = line->etoken) == NULL)
    {
        return (-1);
    }

    while (token != NULL)
    {
        if (id == NULL)
        {
            if ((token->type == TYPE_TOKEN_VECTOR) && (token->index == index))
            {
                strcpy(value, token->value);
                return (0);
            }
        }
        else
        {
            if ((token->type == TYPE_TOKEN_SCALAR) && (token->index == index) && !STRCMP(token->name, id))
            {
                strcpy(value, token->value);
                return (0);
            }
        }
        
        token = token->snext;
    }

    return (-1);
}

int CCConfig::fSetValue(const char *section, const char *id, const char *value, int index)
{
    stLine *line;
    stToken *token;
    int i;
    unsigned int len;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if ((section == NULL) || (value == NULL))
    {
        return (-1);
    }
    
    for (i = 0, line =  *sections; line != NULL; line = *(sections + (++i)))
    {
        if (!STRCMP(section, line->token->name))
        {
            break;
        }
    }
    
    if (line == NULL)
    {
        return (-1);
    }
    
    if ((token = line->etoken) == NULL)
    {
        return (-1);
    }

    while (token != NULL)
    {
        if (((id == NULL) && (token->type == TYPE_TOKEN_VECTOR) && (token->index == index)) || ((id != NULL) && (token->type == TYPE_TOKEN_SCALAR) && (token->index == index) && !STRCMP(token->name, id)))
        {
            if (!STRCMP(token->value, value))
            {
                return (0);
            }
            
            len = strlen(value);
            
            if (len <= strlen(token->value))
            {
                strcpy(token->value, value);
            }
            else
            {
                delete token->value;
                token->value = new char[len + 1];
                
                if (token->value == NULL)
                {
//                    ERRLOG(NULL, 0, "CCConfig::fSetValue: memory allocation fail.");
                    return (-1);
                }
                
                memcpy(token->value, value, len);
                token->value[len] = '\0';
            }
            
            if (iWriteNow)
            {
                fSaveMeToFile();
            }
            else
            {
                iDirtyFlag = 1;
            }
            
            return (0);
        }
        
        token = token->snext;
    }

    return (-1);
}

int CCConfig::fGetCount(const char *section, const char *id)
{
    stLine *line;
    stToken *token;
    int index, i;

    /* in case of non-vital error condition, we should return 0. */

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if (section == NULL)
     /* parameter error. */
    {
        return (-1);
    }

    for (i = 0, line =  *sections; line != NULL; line = *(sections + (++i)))
    {
        if (!STRCMP(section, line->token->name))
        {
            break;
        }
    }
    
    if (line == NULL) /* section not found. */
    {
        return (0);
    }

    if ((token = line->etoken) == NULL)    /* section empty. */
    {
        return (0);
    }

    if (id == NULL)
    {
        if (token->type != TYPE_TOKEN_VECTOR) /* token type confused. */
        {
            return (-1);
        }
        
        while (token->snext != NULL)
        {
            token = token->snext;
        }
        
        return (token->index);
    }
    else
    {
        if (token->type == TYPE_TOKEN_SCALAR)
        {
            for (index = 0; token != NULL; token = token->snext)
            {
                if (!STRCMP(token->name, id))
                {
                    index = token->index;
                }
            }
            
            return (index);
        }
        
        if (token->type == TYPE_TOKEN_VECTOR)
        {
            for (index = 0; token != NULL; token = token->snext)
            {
                if (!STRCMP(token->value, id))
                {
                    index++;
                }
            }
            
            return (index);
        }
    }

    return (-1);
}

int CCConfig::fGetIndex(const char *section, const char *id, int start)
{
    stLine *line;
    stToken *token;
    int index, i;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if (section == NULL)
    {
        return (-1);
    }
    
    for (i = 0, line =  *sections; line != NULL; line = *(sections + (++i)))
    {
        if (!STRCMP(section, line->token->name))
        {
            break;
        }
    }
    
    if (line == NULL)
    {
        return (-1);
    }
    
    if (id == NULL)
    {
        return (i + 1);
    }
    
    if ((token = line->etoken) == NULL)
    {
        return (-1);
    }

    if (token->type == TYPE_TOKEN_SCALAR)
    {
        for (index = 1; token != NULL; token = token->snext, index++)
        {
            if (!STRCMP(token->name, id) && (index > start))
            {
                return (index);
            }
        }
        
        return (-1);
    }
    
    if (token->type == TYPE_TOKEN_VECTOR)
    {
        for (index = 1; token != NULL; token = token->snext, index++)
        {
            if (!STRCMP(token->value, id) && (index > start))
            {
                return (index);
            }
        }
        
        return (-1);
    }

    return (-1);
}

int CCConfig::fAddToken(const char *section, int where, const char *id, const char *value)
{
    stLine *line,  *curline,  **tmpsections;
    stToken *token,  *curtoken;
    int i, iWhat, iWhere;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if (section == NULL)
    {
        return (-1);
    }
    
    if (value == NULL)
    {
        iWhat = TYPE_SECTION;
    }
    else
    {
        if (id == NULL)
        {
            iWhat = TYPE_TOKEN_VECTOR;
        }
        else
        {
            iWhat = TYPE_TOKEN_SCALAR;
        }
    }
    
    if (iWhat == TYPE_SECTION)
    {
        i = iTotalSections;
    }
    
    else if (iWhat == TYPE_TOKEN_VECTOR)
    {
        i = fGetCount(section);
    }
    
    else if (TYPE_TOKEN_SCALAR)
    {
        i = fGetCount(section, id);
    }
    else
    {
        return (-1);
    }
    
    if (i ==  - 1)
    {
        iWhere = 0;
    }
    else
    {
        if ((where < 0) || (where > i))
        {
            iWhere = i;
        }
        else
        {
            iWhere = where;
        }
    }

    token = NULL;
    line = NULL;
    
    if ((token = new stToken) == NULL)
    {
//        ERRLOG(NULL, 0, "memory allocation fail.");
        return (-1);
    }
    
    token->type = iWhat;
    token->index = iWhere + 1;
    
    if (iWhat == TYPE_TOKEN_VECTOR)
    {
        token->name = NULL;
    }
    else
    {
        if (iWhat == TYPE_SECTION)
        {
            i = strlen(section);
        }
        else
        {
            i = strlen(id);
        }
        
        token->name = new char[i + 1];
        
        if (token->name == NULL)
        {
//            ERRLOG(NULL, 0, "memory allocation fail.");
            delete token;
            return (-1);
        }
        
        memcpy(token->name, (iWhat == TYPE_SECTION) ? section : id, i);
        token->name[i] = '\0';
    }
    
    if (iWhat == TYPE_SECTION)
    {
        token->value = NULL;
    }
    else
    {
        i = strlen(value);
        token->value = new char[i + 1];
        
        if (token->value == NULL)
        {
//            ERRLOG(NULL, 0, "memory allocation fail.");
            delete token->name;
            delete token;
            return (-1);
        }
        
        memcpy(token->value, value, i);
        token->value[i] = '\0';
    }
    
    token->next = token->snext = NULL;

    if ((line = new stLine) == NULL)
    {
//        ERRLOG(NULL, 0, "memory allocation fail.");
        delete token->name;
        delete token->value;
        delete token;
        return (-1);
    }
    
    line->next = NULL;
    line->prev = NULL;
    line->token = token;
    line->etoken = NULL;

    token->line = line;

#define SUCCESS_EXIT \
{ \
    if(iWriteNow) \
        fSaveMeToFile(); \
    else \
        iDirtyFlag = 1; \
    return(0); \
}

#define HCLERROR_EXIT(msg) \
{ \
    delete token->name; \
    delete token->value; \
    delete token; \
    delete line; \
    return(-1); \
}

    if (iWhat == TYPE_SECTION)
    {
        tmpsections = new stLine *[iTotalSections + 2];
    
        if (tmpsections == NULL)
        {
            HCLERROR_EXIT("memory allocation.");
        }

        for (i = 0; i < iTotalSections; i++)
        {
            if (!STRCMP((*(sections + i))->token->name, token->name))
            {
                delete []tmpsections;
                HCLERROR_EXIT("Section already exists.");
            }
        }

        if (iTotalSections == 0)
        {
            if (lines == NULL)
            {
                lines = line;
            }
            else
            {
                curline = lines;
                while (curline->next != NULL)
                {
                    curline = curline->next;
                }
                curline->next = line;
                line->prev = curline;
            }
        }
        else
        {
            if (lines == *(sections + iWhere))
            {
                lines = line;
                line->next = *(sections + iWhere);
                (*(sections + iWhere))->prev = line;
            }
            else
            {
                curline = lines;
                
                while (curline->next != *(sections + iWhere))
                {
                    curline = curline->next;
                }
                
                while ((curline != NULL) && ((curline->token->type == TYPE_COMMENT) || (curline->token->type == TYPE_BLANK)))
                {
                    curline = curline->prev;
                }
                
                if (curline == NULL)
                {
                    lines->prev = line;
                    line->next = lines;
                    lines = line;
                }
                else
                {
                    line->prev = curline;
                    line->next = curline->next;
                    
                    if (curline->next != NULL)
                    {
                        curline->next->prev = line;
                    }
                    curline->next = line;
                }
            }
        }
        
        iTotalSections++;
        
        for (i = 0; i < iWhere; i++)
        {
            *(tmpsections + i) = *(sections + i);
        }
        
        *(tmpsections + iWhere) = line;
        
        for (i = iWhere + 1; i < iTotalSections; i++)
        {
            *(tmpsections + i) = *(sections + i - 1);
        }
        
        *(tmpsections + iTotalSections) = NULL;
        
        delete []sections;
        
        sections = tmpsections;
        SUCCESS_EXIT;
    }

    if ((sections == NULL) || (lines == NULL))
    {
        HCLERROR_EXIT("unknown error.");
    }
    
    for (i = 0; i < iTotalSections; i++)
    {
        if (!STRCMP(section, (*(sections + i))->token->name))
        {
            break;
        }
    }
    
    if (i == iTotalSections)
    {
        HCLERROR_EXIT("cannot find the section.");
    }

    curline = *(sections + i);
    curtoken = curline->etoken;
    
    if ((curtoken != NULL) && (curtoken->type != iWhat))
    {
        HCLERROR_EXIT("cannot mix type scalar/vector in a section.");
    }
    
    if ((curtoken == NULL) && (iWhere != 0))
    {
        HCLERROR_EXIT("No tokens in this section, where should be 0.");
    }

    if (iWhat == TYPE_TOKEN_VECTOR)
    {
        if (curtoken == NULL)
        {
            line->next = curline->next;
            line->prev = curline;
            
            if (curline->next != NULL)
            {
                curline->next->prev = line;
            }
            curline->next = line;
            curline->etoken = token;
            SUCCESS_EXIT;
        }
        
        if (iWhere == 0)
        {
            token->snext = curtoken;
            token->next = curtoken;
            token->line = curtoken->line;
            curtoken->line->token = token;
            curline->etoken = token;
            curtoken = token;
            
            while ((token = token->snext) != NULL)
            {
                token->index++;
            }
            
            delete line;
            
            SUCCESS_EXIT;
        }
        
        while ((curtoken != NULL) && (curtoken->index != iWhere))
        {
            curtoken = curtoken->snext;
        }
        
        if (curtoken == NULL)
        {
            HCLERROR_EXIT("Specified position not found.");
        }
        
        token->snext = curtoken->snext;
        token->next = curtoken->next;
        token->line = curtoken->line;
        curtoken->snext = token;
        curtoken->next = token;
        
        while ((token = token->snext) != NULL)
        {
            token->index++;
        }
        
        delete line;
        SUCCESS_EXIT;
    }

    if (iWhat == TYPE_TOKEN_SCALAR)
    {
        if (curtoken == NULL)
        {
            line->next = curline->next;
            line->prev = curline;
            
            if (curline->next != NULL)
            {
                curline->next->prev = line;
            }
            
            curline->next = line;
            curline->etoken = token;
            SUCCESS_EXIT;
        }
        
        if (iWhere == 0)
        {
            line->next = curline->next;
            line->prev = curline;
            
            if (curline->next != NULL)
            {
                curline->next->prev = line;
            }
            
            curline->next = line;
            token->snext = curline->etoken;
            curline->etoken = token;
            
            while ((token = token->snext) != NULL)
            {
                if (!STRCMP(id, token->name))
                {
                    token->index++;
                }
            }
            
            SUCCESS_EXIT;
        }
        
        while ((curline != NULL) && (curline != *(sections + i + 1)))
        {
            if (curline->token->type == TYPE_TOKEN_SCALAR)
            {
                curtoken = curline->token;
                if (!STRCMP(curtoken->name, id) && (curtoken->index == iWhere))
                {
                    break;
                }
            }
            
            curline = curline->next;
        }
        
        if ((curline == NULL) || (curline == *(sections + i + 1)))
        {
            HCLERROR_EXIT("Specified position not found.");
        }

        line->next = curline->next;
        line->prev = curline;
        
        if (curline->next != NULL)
        {
            curline->next->prev = line;
        }
        
        curline->next = line;
        token->snext = curtoken->snext;
        curtoken->snext = token;
        
        while ((token = token->snext) != NULL)
        {
            if (!STRCMP(id, token->name))
            {
                token->index++;
            }
        }
        
        SUCCESS_EXIT;
    }

    HCLERROR_EXIT("Can program run to here?");
}

int CCConfig::fAddComment(const char *comment, int where, const char *section, const char *id)
{
    stLine *line,  *curline;
    stToken *token,  *curtoken;
    int i;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if (where < 0)
    {
        return (-1);
    }

    if ((section == NULL) && (id == NULL))
    {
        // add after the 'where' line.
        if ((lines == NULL) || (where == 0))
        {
            curline = NULL;
        }
        else
        {
            curline = lines;
            
            for (i = 1; i < where; i++)
            {
                if (curline->next == NULL)
                {
                    break;
                }
                
                curline = curline->next;
            }
        }
    }
    else if ((section != NULL) && (id == NULL))
    {
        // add before the definition of this section.
        for (i = 0; i < iTotalSections; i++)
        {
            if (!STRCMP((*(sections + i))->token->name, section))
            {
                break;
            }
        }
        
        if (i == iTotalSections)
        {
            return (-1);
        }
        
        curline = (*(sections + i))->prev;
    }
    else if ((section != NULL) && (id != NULL))
    {
        // add before the 'where'th definition of 'id' in 'section'.
        for (i = 0; i < iTotalSections; i++)
        {
            if (!STRCMP((*(sections + i))->token->name, section))
            {
                break;
            }
        }
        
        if (i == iTotalSections)
        {
            return (-1);
        }
        
        curline = *(sections + i);
        curtoken = curline->etoken;
        
        if ((curtoken == NULL) || (curtoken->type != TYPE_TOKEN_SCALAR))
        {
            return (-1);
        }
        
        while (curtoken != NULL)
        {
            if ((curtoken->index == where) && !STRCMP(curtoken->name, id))
            {
                break;
            }
            
            curtoken = curtoken->snext;
        }
        
        if (curtoken == NULL)
        {
            return (-1);
        }
        
        curline = curtoken->line->prev;
    }
    else
    {
        return (-1);
    }

    if ((token = new stToken) == NULL)
    {
//        ERRLOG(NULL, 0, "fAddComment: memory allocation fail.");
        return (-1);
    }
    
    token->type = TYPE_COMMENT;
    token->index = 0;
    token->value = NULL;
    
    if (comment != NULL)
    {
        i = strlen(comment);
        token->name = new char[i + 2];
        
        if (token->name == NULL)
        {
            delete token;
//            ERRLOG(NULL, 0, "fAddComment: memory allocation fail.");
            return (-1);
        }
        
        memcpy(&token->name[1], comment, i);
        token->name[0] = '#';
        token->name[i + 1] = '\0';
    }
    else
    {
        token->type = TYPE_BLANK;
        token->name = NULL;
    }

    token->next = token->snext = NULL;

    if ((line = new stLine) == NULL)
    {
        delete token->name;
        delete token;
//        ERRLOG(NULL, 0, "fAddComment: memory allocation fail.");
        return (-1);
    }
    
    line->token = token;
    line->etoken = NULL;
    line->next = line->prev = NULL;
    token->line = line;

    if (curline == NULL)
    {
        if (lines == NULL)
        {
            lines = line;
        }
        else
        {
            line->next = lines;
            lines->prev = line;
            lines = line;
        }
    }
    else
    {
        line->next = curline->next;
        line->prev = curline;
        
        if (curline->next != NULL)
        {
            curline->next->prev = line;
        }
        curline->next = line;
    }
    
    if (iWriteNow)
    {
        fSaveMeToFile();
    }
    else
    {
        iDirtyFlag = 1;
    }

    return (0);
}

int CCConfig::fDelToken(const char *section, const char *id, int index)
{
    int i;
    stToken *token,  *ptoken,  *pltoken;
    stLine *line;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if (section == NULL)
    {
        return (-1);
    }

    for (i = 0; i < iTotalSections; i++)
    {
        if (!STRCMP(section, (*(sections + i))->token->name))
        {
            break;
        }
    }
    
    if (i == iTotalSections)
    {
//        ERRLOG(NULL, 0, "section [%s] not found.", section);
        return (-1);
    }

    token = (*(sections + i))->etoken;
    
    if (token == NULL)
    {
//        ERRLOG(NULL, 0, "section [%s] is empty.", section);
        return (-1);
    }

    if (((id == NULL) && (token->type != TYPE_TOKEN_VECTOR)) || ((id != NULL) && (token->type != TYPE_TOKEN_SCALAR)))
    {
//        ERRLOG(NULL, 0, "section [%s] token type mismatch.", section);
        return (-1);
    }

    ptoken = token;
    
    while (token != NULL)
    {
        if ((id == NULL) && (token->index == index))
        {
            // we find it!
            line = token->line;
            
            if (ptoken == token)
            {
                (*(sections + i))->etoken = token->snext;
            }
            else
            {
                ptoken->snext = token->snext;
            }
            
            pltoken = line->token;
            
            if (pltoken == token)
            {
                line->token = token->next;
            }
            else
            {
                while (pltoken != NULL)
                {
                    if (pltoken->next == token)
                    {
                        break;
                    }
                    pltoken = pltoken->next;
                }
                
                if (pltoken == NULL)
                {
                    return (-1);
                }
                
                pltoken->next = token->next;
            }
            
            delete token->name;
            delete token->value;
            delete token;
            
            if (line->token == NULL)
            {
                line->prev->next = line->next;
                
                if (line->next != NULL)
                {
                    line->next->prev = line->prev;
                }
                
                delete line;
            }
            
            for (token = ptoken->snext; token != NULL; token = token->snext)
            {
                token->index--;
            }
            
            if (iWriteNow)
            {
                fSaveMeToFile();
            }
            else
            {
                iDirtyFlag = 1;
            }
            
            return (0);
        }
        
        if ((id != NULL) && !STRCMP(token->name, id) && (token->index == index))
        {
            line = token->line;
            line->prev->next = line->next;
            
            if (line->next != NULL)
            {
                line->next->prev = line->prev;
            }
            
            if (ptoken == token)
            {
                (*(sections + i))->etoken = token->snext;
            }
            else
            {
                ptoken->snext = token->snext;
            }
            
            delete token->name;
            delete token->value;
            delete token;
            delete line;
            
            for (token = ptoken->snext; token != NULL; token = token->snext)
            {
                if (!STRCMP(token->name, id))
                {
                    token->index--;
                }
            }
                
            if (iWriteNow)
            {
                fSaveMeToFile();
            }
            else
            {
                iDirtyFlag = 1;
            }
            
            return (0);
        }
        
        ptoken = token;
        token = token->snext;
    }

    return (-1);
}

int CCConfig::fDelSection(const char *section)
{
    stLine *pline,  *line,  *nline,  *stopline;
    stToken *token,  *ntoken;
    int i, j;

    if (!iIAmReady)
    {
        return (-1);
    }
    
    if ((sections == NULL) || (lines == NULL))
    {
        return (-1);
    }
    
    if (section == NULL)
    {
        return (-1);
    }
    
    if (iTotalSections < 1)
    {
        return (-1);
    }
    
    for (i = 0; i < iTotalSections; i++)
    {
        if (!STRCMP(section, (*(sections + i))->token->name))
        {
            break;
        }
    }
    
    if (i == iTotalSections)
    {
        return (-1);
    }
    
    line = *(sections + i);
    pline = line->prev;
    
    while ((pline != NULL) && ((pline->token->type == TYPE_COMMENT) || (pline->token->type == TYPE_BLANK)))
    {
        pline = pline->prev;
    }

    stopline = line;
    
    while (stopline->next != *(sections + i + 1))
    {
        stopline = stopline->next;
    }
    
    while ((stopline != line) && ((stopline->token->type == TYPE_COMMENT) || (stopline->token->type == TYPE_BLANK)))
    {
        stopline = stopline->prev;
    }
    
    stopline = stopline->next;

    if (pline == NULL)
    {
        line = lines;
    }
    else
    {
        line = pline->next;
    }
    
    while (line != stopline)
    {
        nline = line->next;
        token = line->token;
        
        while (token != NULL)
        {
            ntoken = token->next;
            delete token->name;
            delete token->value;
            delete token;
            token = ntoken;
        }
        
        delete line;
        line = nline;
    }
    
    if (pline != NULL)
    {
        pline->next = line;
        
        if (line != NULL)
        {
            line->prev = pline;
        }
    }
    else
    {
        lines = line;
        
        if (line != NULL)
        {
            line->prev = NULL;
        }
    }

    for (j = i; *(sections + j + 1) != NULL; j++)
    {
        *(sections + j) = *(sections + j + 1);
    }
    
    *(sections + j) = NULL;

    iTotalSections--;

    if (iWriteNow)
    {
        fSaveMeToFile();
    }
    else
    {
        iDirtyFlag = 1;
    }

    return (0);
}

int CCConfig::fExistSection(const char *section)
{
    int i;

    if (!iIAmReady)
    {
        return (0);
    }
    
    if (section == NULL)
    {
        return (0);
    }
    
    for (i = 0; i < iTotalSections; i++)
    {
        if (!STRCMP(section, (*(sections + i))->token->name))
        {
            break;
        }
    }
    
    if (i == iTotalSections)
    {
        return (0);
    }

    return (1);
}

int CCConfig::fTestMe(void)
{
    stLine *line;
    stToken *token;
    char caType[6][32] ={
        "", "TYPE_SECTION", "TYPE_TOKEN_SCALAR", "TYPE_TOKEN_VECTOR", "TYPE_COMMENT", "TYPE_BLANK"
    };
    
    char caBuffer[512];

    if (!iIAmReady)
    {
        return (-1);
    }

    for (line = lines; line != NULL; line = line->next)
    {
//        LOG(NULL, 0, "Line %x (token %x, etoken %x, prev %x, next %x).", line, line->token, line->etoken, line->prev, line->next);
        
        for (token = line->token; token != NULL; token = token->next)
        {
            sprintf(caBuffer, "Token %p (next %p, snext %p, line %p).\n(type %s, index %d)\n name %p \"%s\"\nvalue %p \"%s\"", token, token->next, token->snext, token->line, caType[token->type], token->index, token->name, (token->name ? token->name: ""), token->value, (token->value ? token->value: ""));
//            LOG(NULL, 0, caBuffer);
        }
    }

    return (0);
}



