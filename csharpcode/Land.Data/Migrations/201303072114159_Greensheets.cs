namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Greensheets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Greensheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        OfferId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.LandOfferInterestTypes", t => t.InterestId, cascadeDelete: true)
                .ForeignKey("dbo.SiteOffers", t => t.OfferId)
                .Index(t => t.SiteId)
                .Index(t => t.ContactId)
                .Index(t => t.InterestId)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.LandOfferInterestTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(maxLength: 3),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        NodeDate = c.DateTime(nullable: false),
                        Greensheet_Id = c.Int(),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Greensheets", t => t.Greensheet_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Greensheet_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Taggroup = c.String(),
                        EventDateTime = c.DateTime(),
                        ReminderDateTime = c.DateTime(),
                        AcknowledgeReminder = c.Boolean(nullable: false),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Greensheet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Greensheets", t => t.Greensheet_Id)
                .Index(t => t.Greensheet_Id);
            
            AddColumn("dbo.Contacts", "IsDeveloper", c => c.Boolean());
            AddColumn("dbo.Letters", "Greensheet_Id", c => c.Int());
            AddForeignKey("dbo.Letters", "Greensheet_Id", "dbo.Greensheets", "Id");
            CreateIndex("dbo.Letters", "Greensheet_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Events", new[] { "Greensheet_Id" });
            DropIndex("dbo.Letters", new[] { "Greensheet_Id" });
            DropIndex("dbo.Notes", new[] { "Event_Id" });
            DropIndex("dbo.Notes", new[] { "Greensheet_Id" });
            DropIndex("dbo.Greensheets", new[] { "OfferId" });
            DropIndex("dbo.Greensheets", new[] { "InterestId" });
            DropIndex("dbo.Greensheets", new[] { "ContactId" });
            DropIndex("dbo.Greensheets", new[] { "SiteId" });
            DropForeignKey("dbo.Events", "Greensheet_Id", "dbo.Greensheets");
            DropForeignKey("dbo.Letters", "Greensheet_Id", "dbo.Greensheets");
            DropForeignKey("dbo.Notes", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Notes", "Greensheet_Id", "dbo.Greensheets");
            DropForeignKey("dbo.Greensheets", "OfferId", "dbo.SiteOffers");
            DropForeignKey("dbo.Greensheets", "InterestId", "dbo.LandOfferInterestTypes");
            DropForeignKey("dbo.Greensheets", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Greensheets", "SiteId", "dbo.Sites");
            DropColumn("dbo.Letters", "Greensheet_Id");
            DropColumn("dbo.Contacts", "IsDeveloper");
            DropTable("dbo.Events");
            DropTable("dbo.Notes");
            DropTable("dbo.LandOfferInterestTypes");
            DropTable("dbo.Greensheets");
        }
    }
}
