using BooksDAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDAL.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<BookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            CommandTimeout = int.MaxValue;

        }

        
    }
}
