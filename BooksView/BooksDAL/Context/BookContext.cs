using BooksDAL.Entities;
using BooksDAL.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDAL.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext()
            : base("name=myConnectionString")
        {
        }
        public BookContext(string connStr)
            : base(connStr)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
