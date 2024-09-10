using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Services
{
    public class BookService : IBookService
    {
        private static readonly List<Book> Books = new List<Book>
        {
           new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling" },
            new Book { Id = 2, Title = "A Game of Thrones", Author = "George R.R. Martin" },
            new Book { Id = 3, Title = "The Hobbit", Author = "J.R.R. Tolkien" },
            new Book { Id = 4, Title = "Murder on the Orient Express", Author = "Agatha Christie" },
            new Book { Id = 5, Title = "Foundation", Author = "Isaac Asimov" },
            new Book { Id = 6, Title = "The Shining", Author = "Stephen King" },
            new Book { Id = 7, Title = "The Da Vinci Code", Author = "Dan Brown" },
            new Book { Id = 8, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
            new Book { Id = 9, Title = "Pride and Prejudice", Author = "Jane Austen" },
            new Book { Id = 10, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" }
        };

        public Task<Book> GetBookAsync(int id) => Task.FromResult(Books.FirstOrDefault(b => b.Id == id));

        public Task<Book> CreateBookAsync(Book book)
        {
            book.Id = Books.Max(b => b.Id) + 1;
            Books.Add(book);
            return Task.FromResult(book);
        }

        public Task<bool> UpdateBookAsync(int id, Book book)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null) return Task.FromResult(false);

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteBookAsync(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return Task.FromResult(false);

            Books.Remove(book);
            return Task.FromResult(true);
        }
    }
}