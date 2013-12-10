namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "AddressCountry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "AddressCountry");
        }
    }
}
