namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieNumberAvailble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailble", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailble");
        }
    }
}
