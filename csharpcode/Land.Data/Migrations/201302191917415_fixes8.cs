namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixes8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SiteExchanges", "BrokerSolicitorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteExchanges", "BrokerSolicitorId", c => c.Int());
        }
    }
}
