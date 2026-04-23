using BookManager.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Repository.Models;
using System.Text.Json;

namespace BookManager.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string _filePath = "../../../../BookManager.Data/BookList.json";
        private readonly List<Book> _books;


        public BookRepository()
        {
            _books = LoadData(_filePath).ToList();
        }

        public List<Book> GetBooks()
        {
            return _books.ToList();
        }

        public Book GetSingleBook(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public Book GetBookWithName(string name)
        {
            return _books.FirstOrDefault(b => b.Name == name);
        }

        public int AddBook(Book newBook)
        {
            newBook.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(newBook);

            SaveData();
            return newBook.Id;
        }

        public int UpdateBook(Book book)
        {
            bool updated = false;

            var index = _books.FindIndex(b => b.Id == book.Id);
            if(index >= 0)
            {
                _books[index] = book;
                updated = true;
            }

            if (updated) SaveData();

            return book.Id;
        }

        public int DeleteBook(int id)
        {
            Book book;

            book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return -1;

            _books.Remove(book);

            SaveData();
            return book.Id;
        }

        #region Helper Methods
        public IEnumerable<Book> LoadData(string filePath)
        {
            if(!File.Exists(filePath)) return new List<Book>();

            var json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<Book>();

            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void SaveData()
        {
            List<Book> snapshot;

            snapshot = _books.ToList();

            var jsonPayLoad = JsonSerializer.Serialize(snapshot, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            using var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write,FileShare.None, bufferSize:8192);

            var bytes = Encoding.UTF8.GetBytes(jsonPayLoad);
            fs.Write(bytes , 0, bytes.Length);
            fs.Flush();
        }
        #endregion
    }
}
