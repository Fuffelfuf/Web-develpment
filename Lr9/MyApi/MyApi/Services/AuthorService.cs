using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Services
{
    public class AuthorService : IAuthorService
    {
        private static readonly List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling" },
            new Author { Id = 2, Name = "George R.R. Martin" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" },
            new Author { Id = 4, Name = "Agatha Christie" },
            new Author { Id = 5, Name = "Isaac Asimov" },
            new Author { Id = 6, Name = "Stephen King" },
            new Author { Id = 7, Name = "Dan Brown" },
            new Author { Id = 8, Name = "Harper Lee" },
            new Author { Id = 9, Name = "Jane Austen" },
            new Author { Id = 10, Name = "F. Scott Fitzgerald" }
        };

        public Task<Author> GetAuthorAsync(int id) => Task.FromResult(Authors.FirstOrDefault(a => a.Id == id));

        public Task<Author> CreateAuthorAsync(Author author)
        {
            author.Id = Authors.Max(a => a.Id) + 1;
            Authors.Add(author);
            return Task.FromResult(author);
        }

        public Task<bool> UpdateAuthorAsync(int id, Author author)
        {
            var existingAuthor = Authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null) return Task.FromResult(false);

            existingAuthor.Name = author.Name;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAuthorAsync(int id)
        {
            var author = Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return Task.FromResult(false);

            Authors.Remove(author);
            return Task.FromResult(true);
        }
    }
}