using PBTech.Domain.Interfaces;
using PBTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBTech.APIv1Test
{
    public class NewsLetterServiceFake : INewsLetterRepository
    {
        private List<ReceiveNews> receiveList;

        public NewsLetterServiceFake()
        {
            receiveList = new List<ReceiveNews>();
            receiveList.Add(new ReceiveNews(1,"Pessoa 1","pessoa1@teste.com.br"));
            receiveList.Add(new ReceiveNews(2,"Pessoa 2","pessoa2@teste.com.br"));
            receiveList.Add(new ReceiveNews(3,"Pessoa 3","pessoa3@teste.com.br"));
            receiveList.Add(new ReceiveNews(4,"Pessoa 4","pessoa4@teste.com.br"));
            receiveList.Add(new ReceiveNews(5,"Pessoa 5","pessoa5@teste.com.br"));
        }

        public async Task<ReceiveNews> Add(ReceiveNews Received)
        {
            receiveList.Add(Received);
            return Received;
        }

        public async Task Delete(ReceiveNews Received)
        {
            var receive = receiveList.First(x => x.Email == Received.Email);
            receiveList.Remove(receive);
        }

        public async Task<List<ReceiveNews>> GetAll()
        {
            return receiveList.ToList();
        }

        public async Task<ReceiveNews?> GetByEmail(string Email)
        {
            var receive = receiveList.FirstOrDefault(x => x.Email.Equals(Email));
            return receive;
        }

        public async Task<ReceiveNews?> GetById(int Id)
        {
            var receive = receiveList.FirstOrDefault(x => x.Id.Equals(Id));
            return receive;
        }

        public async Task Update(ReceiveNews Received)
        {
            var receive = receiveList.First(x => x.Email == Received.Email);
            receiveList.Remove(receive);
        }
    }
}
