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
        Task<List<GetBooksDTO>> GetAllBooksAsync();

        Task<Book> GetBookByNameAsync(string name);
        Task<int> AddBookAsync(CreateBookDTO dto);
        Task<int> UpdateBookAsync(UpdateBookDTO dto);
        Task<int> DeleteBookAsync(int id);
    }
}
