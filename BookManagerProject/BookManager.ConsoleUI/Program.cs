using BookManager.Repository;
using BookManager.Service;
using BookManager.Service.DTOs;

namespace BookManager.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bookRepo = new BookRepository();
            var service = new BookService(bookRepo);

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
                    AddBook(service);
                    break;
                case "2":
                    GetAllBooks(service);
                    break;
                case "3":
                    SearchBook(service);
                    break;
                case "4":
                    UpdateBook(service);
                    break;
                case "5":
                    DeleteBook(service);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        static void AddBook(BookService service)
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Book Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter Book Publishing Date: ");
            if(!int.TryParse(Console.ReadLine(), out int date))
            {
                Console.WriteLine("Invalid format");
                return;
            }

            var dto = new CreateBookDTO
            {
                Name = title,
                Author = author,
                PublishingYear = date
            };

            service.AddBook(dto);

            Console.WriteLine("Book Added");
        }

        static void GetAllBooks(BookService service)
        {
            var books = service.GetAllBooks();

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

        static void SearchBook(BookService service)
        {
            Console.Write("Enter the book title: ");
            string title = Console.ReadLine();

            var books = service.GetBookByName(title);

            if(books == null)
            {
                Console.WriteLine("Book not found");
            }
            else
            {
                Console.WriteLine($"Book ID:{books.Id}. {books.Name} by {books.Author} published in: {books.PublishingYear}");
            }
        }

        static void UpdateBook(BookService service)
        {
            Console.Write("Enter title of book to update: ");
            string title = Console.ReadLine();

            var books = service.GetBookByName(title);

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

            Console.Write("Enter new publishing year: ");
            if(!int.TryParse(Console.ReadLine(), out int newYear))
            {
                Console.WriteLine("Invalid format");
                return;
            }

            var dto = new UpdateBookDTO
            {
                Id = books.Id,
                Name = newName,
                Author = newAuthor,
                PublishingYear = newYear,
            };

            int updated = service.UpdateBook(dto);

            if(updated == 1)
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Update failed");
            }
        }

        static void DeleteBook(BookService service)
        {
            Console.Write("Enter title of book to delete: ");
            string title = Console.ReadLine();

            var books = service.GetBookByName(title);

            if(books == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            int deleted = service.DeleteBook(books.Id);

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
