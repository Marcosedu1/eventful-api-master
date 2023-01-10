using System.Data.Entity.Migrations;

namespace eventful_api_master.Migrations
{
    public class CreateTableUser : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Users", c => new
            {
                Id = c.Int(nullable: false, identity: true),
                firstName = c.String(nullable: false),
                lastName = c.String(nullable: false),
                email = c.String(nullable: false),
                cpf = c.String(nullable: false),
                birthdate = c.DateTime(nullable: false),
                genre = c.Int(nullable: false),
                acceptedTerms = c.Boolean(nullable: false),
                active = c.Boolean(nullable: false),
                creationDate = c.DateTime(nullable: false),
                changeDate = c.DateTime(nullable: false),
                creationUser = c.Int(nullable: false),
                changeUser = c.Int(nullable: false),
            });
        }
    }
}
