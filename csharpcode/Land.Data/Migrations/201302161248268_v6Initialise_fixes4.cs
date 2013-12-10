namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SiteExchanges", "BrokerSolicitorId", "dbo.Contacts");
            DropForeignKey("dbo.SiteExchanges", "SiteOfferId", "dbo.SiteOffers");
            DropIndex("dbo.SiteExchanges", new[] { "BrokerSolicitorId" });
            DropIndex("dbo.SiteExchanges", new[] { "SiteOfferId" });
            AddColumn("dbo.Contacts", "IsBuyer", c => c.Boolean());
            AddColumn("dbo.SiteExchanges", "BuyerId", c => c.Int());
            AddColumn("dbo.SiteExchanges", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteExchanges", "SiteOfferId", c => c.Int());
            AddForeignKey("dbo.SiteExchanges", "BuyerId", "dbo.Contacts", "Id");
            AddForeignKey("dbo.SiteExchanges", "SiteOfferId", "dbo.SiteOffers", "Id");
            AddForeignKey("dbo.SiteExchanges", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
            CreateIndex("dbo.SiteExchanges", "BuyerId");
            CreateIndex("dbo.SiteExchanges", "SiteOfferId");
            CreateIndex("dbo.SiteExchanges", "SiteId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SiteExchanges", new[] { "SiteId" });
            DropIndex("dbo.SiteExchanges", new[] { "SiteOfferId" });
            DropIndex("dbo.SiteExchanges", new[] { "BuyerId" });
            DropForeignKey("dbo.SiteExchanges", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SiteExchanges", "SiteOfferId", "dbo.SiteOffers");
            DropForeignKey("dbo.SiteExchanges", "BuyerId", "dbo.Contacts");
            AlterColumn("dbo.SiteExchanges", "SiteOfferId", c => c.Int(nullable: false));
            DropColumn("dbo.SiteExchanges", "SiteId");
            DropColumn("dbo.SiteExchanges", "BuyerId");
            DropColumn("dbo.Contacts", "IsBuyer");
            CreateIndex("dbo.SiteExchanges", "SiteOfferId");
            CreateIndex("dbo.SiteExchanges", "BrokerSolicitorId");
            AddForeignKey("dbo.SiteExchanges", "SiteOfferId", "dbo.SiteOffers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SiteExchanges", "BrokerSolicitorId", "dbo.Contacts", "Id");
        }
    }
}
