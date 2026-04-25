using BookManager.Service.DTOs;
using BookManager.Service.Interfaces;
using BookManager.Repository.Interfaces;
using BookManager.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<GetBooksDTO>> GetAllBooksAsync()
        {
            var books = _bookRepository.GetBooks();

            return books.Select(b => new GetBooksDTO
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                PublishingYear = b.PublishingYear,
            }).ToList();
        }

        public async Task<Book> GetBookByNameAsync(string name)
        {
            return _bookRepository.GetBookWithName(name);
        }

        public async Task<int> AddBookAsync(CreateBookDTO dto)
        {
            var newBook = new Book
            {
                Name = dto.Name,
                Author = dto.Author,
                PublishingYear = dto.PublishingYear,
            };

            return await _bookRepository.AddBookAsync(newBook);
        }

        public async Task<int> UpdateBookAsync(UpdateBookDTO dto)
        {
            var bookToUpdate = _bookRepository.GetBooks()
                .FirstOrDefault(b => b.Id == dto.Id);

            if (bookToUpdate == null) return -1;

            bookToUpdate.Name = dto.Name;
            bookToUpdate.Author = dto.Author;
            bookToUpdate.PublishingYear = dto.PublishingYear;

            await _bookRepository.UpdateBookAsync(bookToUpdate);

            return 1;
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            var bookToDelete = _bookRepository.GetSingleBook(id);
            if(bookToDelete == null) return -1;

            await _bookRepository.DeleteBookAsync(id);
            return 1;
        }
    }
}
