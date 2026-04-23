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

        public List<GetBooksDTO> GetAllBooks()
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

        public Book GetBookByName(string name)
        {
            return _bookRepository.GetBookWithName(name);
        }

        public int AddBook(CreateBookDTO dto)
        {
            var newBook = new Book
            {
                Name = dto.Name,
                Author = dto.Author,
                PublishingYear = dto.PublishingYear,
            };

            return _bookRepository.AddBook(newBook);
        }

        public int UpdateBook(UpdateBookDTO dto)
        {
            var bookToUpdate = _bookRepository.GetBooks()
                .FirstOrDefault(b => b.Id == dto.Id);

            if (bookToUpdate == null) return -1;

            bookToUpdate.Name = dto.Name;
            bookToUpdate.Author = dto.Author;
            bookToUpdate.PublishingYear = dto.PublishingYear;

            _bookRepository.UpdateBook(bookToUpdate);

            return 1;
        }

        public int DeleteBook(int id)
        {
            var bookToDelete = _bookRepository.GetSingleBook(id);
            if(bookToDelete == null) return -1;

            _bookRepository.DeleteBook(id);
            return 1;
        }
    }
}
