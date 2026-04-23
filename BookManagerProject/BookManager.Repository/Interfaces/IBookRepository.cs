using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Repository.Models;

namespace BookManager.Repository.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        Book GetSingleBook(int id);
        Book GetBookWithName(string name);
        int AddBook(Book newBook);
        int UpdateBook(Book book);
        int DeleteBook(int id);
    }
}
