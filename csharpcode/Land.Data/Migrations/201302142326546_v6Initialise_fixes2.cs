namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfferSubjecttoTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SiteOffers", "ConditionalNotes", c => c.String());
            AddColumn("dbo.SiteOffers", "OfferSubjecttoTypeID", c => c.Int());
            AddColumn("dbo.SiteExchanges", "InstructionConfirmedOnDate", c => c.DateTime());
            AddColumn("dbo.SiteExchanges", "ConditionsOfExchange", c => c.String());
            AddForeignKey("dbo.SiteOffers", "OfferSubjecttoTypeID", "dbo.OfferSubjecttoTypes", "Id");
            CreateIndex("dbo.SiteOffers", "OfferSubjecttoTypeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SiteOffers", new[] { "OfferSubjecttoTypeID" });
            DropForeignKey("dbo.SiteOffers", "OfferSubjecttoTypeID", "dbo.OfferSubjecttoTypes");
            DropColumn("dbo.SiteExchanges", "ConditionsOfExchange");
            DropColumn("dbo.SiteExchanges", "InstructionConfirmedOnDate");
            DropColumn("dbo.SiteOffers", "OfferSubjecttoTypeID");
            DropColumn("dbo.SiteOffers", "ConditionalNotes");
            DropTable("dbo.OfferSubjecttoTypes");
        }
    }
}
