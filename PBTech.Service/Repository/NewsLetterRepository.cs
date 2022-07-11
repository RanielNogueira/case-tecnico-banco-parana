using Microsoft.EntityFrameworkCore;
using PBTech.Domain.Interfaces;
using PBTech.Domain.Models;

namespace PBTech.Service.Repository
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        PBTechContexto Context;

        public NewsLetterRepository(PBTechContexto Context)
        {
            this.Context = Context;
        }

        public async Task<ReceiveNews> Add(ReceiveNews Received)
        {
            try
            {
                Context.Entry(Received).State = EntityState.Added;
                await Context.SaveChangesAsync();
                return Received;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(ReceiveNews Received)
        {
            try
            {
                Context.Entry(Received).State = EntityState.Deleted;
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ReceiveNews>> GetAll()
        {
            try
            {
                return await Context.ReceiveNews.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReceiveNews?> GetByEmail(string Email)
        {
            try
            {
                var result = await Context.ReceiveNews.FirstOrDefaultAsync(c => c.Email == Email);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ReceiveNews?> GetById(int Id)
        {
            try
            {
                var result = await Context.ReceiveNews.AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(ReceiveNews Received)
        {
            try
            {
                if (Integrity(Received).Result)
                    throw new Exception("Email existing in another register");

                Context.Entry(Received).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> Integrity(ReceiveNews Receive)
        {
            try
            {
                var result = await Context.ReceiveNews.AsNoTracking().AnyAsync(c => c.Id != Receive.Id && c.Email == Receive.Email);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
