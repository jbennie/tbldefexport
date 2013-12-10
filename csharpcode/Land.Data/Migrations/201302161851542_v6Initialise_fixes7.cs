namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "AddressCountry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "AddressCountry");
        }
    }
}
