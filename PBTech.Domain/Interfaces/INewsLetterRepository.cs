using PBTech.Domain.Models;

namespace PBTech.Domain.Interfaces
{
    public interface INewsLetterRepository
    {
        public Task<ReceiveNews?> GetByEmail(string Email);
        public Task<ReceiveNews?> GetById(int Id);
        public Task<List<ReceiveNews>> GetAll();
        public Task<ReceiveNews> Add(ReceiveNews Received);
        public Task Update(ReceiveNews Received);
        public Task Delete(ReceiveNews Received);
    }
}
