namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteExchanges", "SearchesSubmitedDate", c => c.DateTime());
            AddColumn("dbo.SiteExchanges", "DraftContractSubmitedDate", c => c.DateTime());
            AddColumn("dbo.SiteExchanges", "ChangedBy", c => c.String());
            AddColumn("dbo.SiteExchanges", "CreatedBy", c => c.String());
            AddColumn("dbo.SiteExchanges", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.SiteExchanges", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.SiteExchanges", "SearchesSumbitedDate");
            DropColumn("dbo.SiteExchanges", "DraftContractSubmittedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteExchanges", "DraftContractSubmittedDate", c => c.DateTime());
            AddColumn("dbo.SiteExchanges", "SearchesSumbitedDate", c => c.DateTime());
            DropColumn("dbo.SiteExchanges", "Created");
            DropColumn("dbo.SiteExchanges", "Modified");
            DropColumn("dbo.SiteExchanges", "CreatedBy");
            DropColumn("dbo.SiteExchanges", "ChangedBy");
            DropColumn("dbo.SiteExchanges", "DraftContractSubmitedDate");
            DropColumn("dbo.SiteExchanges", "SearchesSubmitedDate");
        }
    }
}
