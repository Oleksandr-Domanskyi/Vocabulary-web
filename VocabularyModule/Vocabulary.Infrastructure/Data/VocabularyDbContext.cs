using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity;

namespace Vocabulary.Infrastructure.Data
{
    public class VocabularyDbContext : DbContext
    {
        public VocabularyDbContext(DbContextOptions<VocabularyDbContext> options) : base(options)
        {
        }

        public DbSet<VocabularyItem> VocabularyItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

