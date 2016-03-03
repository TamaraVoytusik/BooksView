using BooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDAL.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Book");
        }
    }

}
