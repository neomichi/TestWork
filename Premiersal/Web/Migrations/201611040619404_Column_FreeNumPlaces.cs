namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Column_FreeNumPlaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "FreeNumPlaces", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "FreeNumPlaces");
        }
    }
}
