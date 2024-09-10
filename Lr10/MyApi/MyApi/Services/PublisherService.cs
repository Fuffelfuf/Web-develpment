using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Services
{
    public class PublisherService : IPublisherService
    {
        private static readonly List<Publisher> Publishers = new List<Publisher>
        {
            new Publisher { Id = 1, Name = "Bloomsbury Publishing" },
            new Publisher { Id = 2, Name = "Penguin Random House" },
            new Publisher { Id = 3, Name = "HarperCollins" },
            new Publisher { Id = 4, Name = "Collins Crime Club" },
            new Publisher { Id = 5, Name = "Gnome Press" },
            new Publisher { Id = 6, Name = "Scribner" },
            new Publisher { Id = 7, Name = "Doubleday" },
            new Publisher { Id = 8, Name = "J.B. Lippincott & Co." },
            new Publisher { Id = 9, Name = "Thomas Egerton" },
            new Publisher { Id = 10, Name = "Charles Scribner's Sons" }
        };

        public Task<Publisher> GetPublisherAsync(int id) => Task.FromResult(Publishers.FirstOrDefault(p => p.Id == id));

        public Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            publisher.Id = Publishers.Max(p => p.Id) + 1;
            Publishers.Add(publisher);
            return Task.FromResult(publisher);
        }

        public Task<bool> UpdatePublisherAsync(int id, Publisher publisher)
        {
            var existingPublisher = Publishers.FirstOrDefault(p => p.Id == id);
            if (existingPublisher == null) return Task.FromResult(false);

            existingPublisher.Name = publisher.Name;
            return Task.FromResult(true);
        }

        public Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return Task.FromResult(false);

            Publishers.Remove(publisher);
            return Task.FromResult(true);
        }
    }
}