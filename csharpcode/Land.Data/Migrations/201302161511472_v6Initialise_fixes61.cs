namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Towns", "referenceId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Towns", "referenceId");
        }
    }
}
