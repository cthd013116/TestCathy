using CathyTest2025.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CathyTest2025.Data
{
    public class ApplicationDbContext  : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CurrencyModel> Currency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrencyModel>()
                .HasKey(c => c.CurrencyEn); // 配置主鍵
            modelBuilder.Entity<CurrencyModel>()
                .ToTable("Currency"); // 配置表名稱
        }
    }
}