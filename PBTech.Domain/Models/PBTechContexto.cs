using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBTech.Domain.Models
{
    public class PBTechContexto : DbContext
    {
        public PBTechContexto(DbContextOptions<PBTechContexto> options) : base(options)
        {
        }
        public DbSet<ReceiveNews> ReceiveNews { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<ReceiveNews>().HasKey(m => m.Id);
            base.OnModelCreating(builder);

        }
    }
}
