namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberInStack");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberInStack", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
