using BookManager.Service.DTOs;
using BookManager.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Service.Interfaces
{
    public interface IBookService
    {
        List<GetBooksDTO> GetAllBooks();

        Book GetBookByName(string name);
        int AddBook(CreateBookDTO dto);
        int UpdateBook(UpdateBookDTO dto);
        int DeleteBook(int id);
    }
}
