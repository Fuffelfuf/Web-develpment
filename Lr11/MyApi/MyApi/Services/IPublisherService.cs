using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Services
{
    public interface IPublisherService
    {
        Task<Publisher> GetPublisherAsync(int id);
        Task<Publisher> CreatePublisherAsync(Publisher publisher);
        Task<bool> UpdatePublisherAsync(int id, Publisher publisher);
        Task<bool> DeletePublisherAsync(int id);
    }
}