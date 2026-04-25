using BookManager.Repository;
using BookManager.Service;
using BookManager.Service.DTOs;

namespace BookManager.ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var bookRepo = await BookRepository.CreateAsync("../../../../BookManager.Data/BookList.json");
            var service = new BookService(bookRepo);

            while (true)
            {
                Console.WriteLine("===========Book Manager===========");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Get All Books");
                Console.WriteLine("3. Search Book By Name");
                Console.WriteLine("4. Update Book");
                Console.WriteLine("5. Delete Book");
                Console.WriteLine("6. Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddBook(service);
                        break;
                    case "2":
                        await GetAllBooks(service);
                        break;
                    case "3":
                        await SearchBook(service);
                        break;
                    case "4":
                        await UpdateBook(service);
                        break;
                    case "5":
                        await DeleteBook(service);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static async Task<int> ReadValidInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                    return result;

                Console.WriteLine("Invalid format. Please enter a valid number.\n");
            }
        }

        static async Task AddBook(BookService service)
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Book Author: ");
            string author = Console.ReadLine();

            int date = await ReadValidInt("Enter Book Publishing Date: ");

            var dto = new CreateBookDTO
            {
                Name = title,
                Author = author,
                PublishingYear = date
            };

            await service.AddBookAsync(dto);

            Console.WriteLine("Book Added");
        }

        static async Task GetAllBooks(BookService service)
        {
            var books = await service.GetAllBooksAsync();

            if (books.Count == 0)
            {
                Console.WriteLine("No Books Found");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"Book ID:{book.Id}. {book.Name} by {book.Author} published in: {book.PublishingYear}");
            }
        }

        static async Task SearchBook(BookService service)
        {
            Console.Write("Enter the book title: ");
            string title = Console.ReadLine();

            var books = await service.GetBookByNameAsync(title);

            if(books == null)
            {
                Console.WriteLine("Book not found");
            }
            else
            {
                Console.WriteLine($"Book ID:{books.Id}. {books.Name} by {books.Author} published in: {books.PublishingYear}");
            }
        }

        static async Task UpdateBook(BookService service)
        {
            Console.Write("Enter title of book to update: ");
            string title = Console.ReadLine();

            var books = await service.GetBookByNameAsync(title);

            if(books == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            Console.WriteLine($"Found: {books.Name} - {books.Author} - {books.PublishingYear}");

            Console.Write("Enter new Title: ");
            string newName = Console.ReadLine();

            Console.Write("Enter new Author: ");
            string newAuthor = Console.ReadLine();

            int newDate = await ReadValidInt("Enter Book Publishing Date: ");

            var dto = new UpdateBookDTO
            {
                Id = books.Id,
                Name = newName,
                Author = newAuthor,
                PublishingYear = newDate,
            };

            int updated = await service.UpdateBookAsync(dto);

            if(updated == 1)
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Update failed");
            }
        }

        static async Task DeleteBook(BookService service)
        {
            Console.Write("Enter title of book to delete: ");
            string title = Console.ReadLine();

            var books = await service.GetBookByNameAsync(title);

            if(books == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            int deleted = await service.DeleteBookAsync(books.Id);

            if (deleted == 1)
            {
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine("Delete failed");
            }
        }
    }
}
