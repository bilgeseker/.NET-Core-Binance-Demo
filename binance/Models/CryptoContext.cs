using System;
using Microsoft.EntityFrameworkCore;


namespace binance.Models
{
    public class CryptoContext : DbContext
    {
        public DbSet<CryptoDetails> Cryptolar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=crypto;user=root;password=x");
        }

    }
}

