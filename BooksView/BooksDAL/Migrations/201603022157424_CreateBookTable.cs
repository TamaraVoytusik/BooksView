namespace BooksDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBookTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        ISBN10 = c.String(),
                        ISBN13 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Book");
        }
    }
}
