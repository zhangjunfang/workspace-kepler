using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientBLL
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime birth { get; set; }
        public bool Sex { get; set; }
        public List<Book> myBookList { get; set; }
    }

    public class Book
    {
        public int BookID { get; set; }
        public List<BookChapter> myBookChapterList { get; set; }
    }

    public class BookChapter
    {
        public int PageNum { get; set; }
        public string ChapterNamer { get; set; }
    }
}
