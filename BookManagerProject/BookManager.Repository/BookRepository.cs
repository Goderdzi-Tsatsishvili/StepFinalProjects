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
        private readonly string _filePath;
        private readonly List<Book> _books;
        private readonly object _lock = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public BookRepository(string filePath, List<Book> books)
        {
            _filePath = filePath;
            _books = books;
        }

        public static async Task<BookRepository> CreateAsync(string filePath)
        {
            var books = new List<Book>();

            await foreach(var book in LoadDataAsync(filePath)) books.Add(book);

            return new BookRepository(filePath, books);
        }

        public List<Book> GetBooks()
        {
            lock (_lock)
            {
                return _books.ToList();
            }
        }

        public Book GetSingleBook(int id)
        {
            lock (_lock)
            {
                return _books.FirstOrDefault(b => b.Id == id);
            }
        }

        public Book GetBookWithName(string name)
        {
            lock (_lock)
            {
                return _books.FirstOrDefault(b => b.Name == name);
            }
        }

        public async Task<int> AddBookAsync(Book newBook)
        {
            lock (_lock)
            {
                newBook.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
                _books.Add(newBook);
            }

            await SaveDataAsync();
            return newBook.Id;
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            bool updated = false;

            lock (_lock)
            {
                var index = _books.FindIndex(b => b.Id == book.Id);
                if (index >= 0)
                {
                    _books[index] = book;
                    updated = true;
                }
            }

            if (updated) await SaveDataAsync();

            return book.Id;
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            Book book;

            lock (_lock)
            {
                book = _books.FirstOrDefault(b => b.Id == id);
                if (book == null) return -1;

                _books.Remove(book);
            }

            await SaveDataAsync();
            return book.Id;
        }

        #region Helper Methods
        public static async IAsyncEnumerable<Book> LoadDataAsync(string filePath)
        {
            if (!File.Exists(filePath)) yield break;

            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, useAsync: true);
            using var ms = new MemoryStream();
            await fs.CopyToAsync(ms);
            ms.Position = 0;

            var json = Encoding.UTF8.GetString(ms.ToArray());

            List<Book> deserialized = null;
            try
            {
                deserialized = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            }
            catch
            {
                yield break;
            }

            if (deserialized == null) yield break;

            foreach(var book in deserialized)
            {
                yield return book;
            }

            //var json = File.ReadAllText(filePath);

            //if (string.IsNullOrWhiteSpace(json))
            //    return new List<Book>();

            //return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        private async Task SaveDataAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                List<Book> snapshot;

                lock (_lock)
                {
                    snapshot = _books.ToList();
                }

                var jsonPayLoad = JsonSerializer.Serialize(snapshot, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                using var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write,FileShare.None, bufferSize:8192, useAsync: true);

                var bytes = Encoding.UTF8.GetBytes(jsonPayLoad);
                await fs.WriteAsync(bytes , 0, bytes.Length);
                await fs.FlushAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }
        #endregion
    }
}
