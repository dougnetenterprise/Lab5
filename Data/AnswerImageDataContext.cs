using Microsoft.EntityFrameworkCore;
using Lab5.Models;

namespace Lab5.Data
{
    public class AnswerImageDataContext : DbContext
    {

        public AnswerImageDataContext(DbContextOptions<AnswerImageDataContext> options) : base(options) 
        {
        }

        public DbSet<AnswerImage> AnswerImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerImage>().ToTable("AnswerImage").HasKey(c => new { c.AnswerImageId});
        }
    }
}

