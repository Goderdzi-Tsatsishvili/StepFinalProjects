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
        Task<int> AddBookAsync(Book newBook);
        Task<int> UpdateBookAsync(Book book);
        Task<int> DeleteBookAsync(int id);
    }
}
