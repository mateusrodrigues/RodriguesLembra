namespace RodriguesLembra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Score");
        }
    }
}
