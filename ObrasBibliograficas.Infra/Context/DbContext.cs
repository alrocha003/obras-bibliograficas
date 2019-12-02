using Microsoft.EntityFrameworkCore;
using ObrasBibliograficas.Domain.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObrasBibliograficas.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
