namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Postcode = c.String(),
                        County = c.String(),
                        Country = c.String(),
                        Latitude = c.String(),
                        Longditude = c.String(),
                        Duplicate = c.Boolean(nullable: false),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        referenceid = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        SiteCreatedDate = c.DateTime(nullable: false),
                        AddressLineOne = c.String(),
                        AddressLineTwo = c.String(),
                        AddressLineThree = c.String(),
                        AddressCounty = c.String(),
                        AddressPostcode = c.String(),
                        Londitude = c.String(),
                        Latitude = c.String(),
                        Hasplanning = c.Boolean(nullable: false),
                        DeadFileNumber = c.String(),
                        DeadFileBoxNumber = c.String(),
                        NegotiatorId = c.Int(),
                        AgentId = c.Int(),
                        OwnerId = c.Int(),
                        TownId = c.Int(),
                        LandCurrentuseTypeId = c.Int(),
                        LandFieldTypeId = c.Int(),
                        LandProjectTypeId = c.Int(),
                        LandProbabilityTypeId = c.Int(),
                        LandPriorityTypeId = c.Int(),
                        LandStatusTypeId = c.Int(),
                        LandSummaryTypeId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.NegotiatorId)
                .ForeignKey("dbo.Contacts", t => t.OwnerId)
                .ForeignKey("dbo.Contacts", t => t.AgentId)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .ForeignKey("dbo.LandCurrentuseTypes", t => t.LandCurrentuseTypeId)
                .ForeignKey("dbo.LandFieldTypes", t => t.LandFieldTypeId)
                .ForeignKey("dbo.LandPriorityTypes", t => t.LandPriorityTypeId)
                .ForeignKey("dbo.LandProbabilityTypes", t => t.LandProbabilityTypeId)
                .ForeignKey("dbo.LandProjectTypes", t => t.LandProjectTypeId)
                .ForeignKey("dbo.LandStatusTypes", t => t.LandStatusTypeId)
                .ForeignKey("dbo.LandSummaryTypes", t => t.LandSummaryTypeId)
                .Index(t => t.NegotiatorId)
                .Index(t => t.OwnerId)
                .Index(t => t.AgentId)
                .Index(t => t.TownId)
                .Index(t => t.LandCurrentuseTypeId)
                .Index(t => t.LandFieldTypeId)
                .Index(t => t.LandPriorityTypeId)
                .Index(t => t.LandProbabilityTypeId)
                .Index(t => t.LandProjectTypeId)
                .Index(t => t.LandStatusTypeId)
                .Index(t => t.LandSummaryTypeId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100),
                        Prefix = c.String(),
                        Initial = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Suffix = c.String(),
                        Title = c.String(),
                        IsStrategic = c.Boolean(nullable: false),
                        OrganisationName = c.String(),
                        Region = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        AddressLine4 = c.String(),
                        AddressCounty = c.String(),
                        AddressPostcode = c.String(),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        MobilePhone = c.String(),
                        FaxNumber = c.String(),
                        AltPhone = c.String(),
                        Switchboard = c.String(),
                        DXNumber = c.String(),
                        DefaultEmail = c.String(),
                        AlternativeEmail = c.String(),
                        Website = c.String(),
                        DDIPhone = c.String(),
                        referenceid = c.Int(),
                        SortOrder = c.Int(),
                        LandRoleTypeId = c.Int(),
                        TownId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsNegotiator = c.Boolean(),
                        IsOwner = c.Boolean(),
                        IsAgent = c.Boolean(),
                        IsPlanningContact = c.Boolean(),
                        IsPurchaser = c.Boolean(),
                        IsVendorSolicitor = c.Boolean(),
                        IsPurchaserSolicitor = c.Boolean(),
                        IsBrokerSolicitor = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .ForeignKey("dbo.LandRoleTypes", t => t.LandRoleTypeId)
                .Index(t => t.TownId)
                .Index(t => t.LandRoleTypeId);
            
            CreateTable(
                "dbo.LandRoleTypes",
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
            
            CreateTable(
                "dbo.Plots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        plotcount = c.Int(nullable: false),
                        LandPlotTypeId = c.Int(),
                        IsSocialHousing = c.Boolean(nullable: false),
                        notes = c.String(),
                        reason = c.String(),
                        comment = c.String(),
                        totalareasqm = c.Double(nullable: false),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .ForeignKey("dbo.LandPlotTypes", t => t.LandPlotTypeId)
                .Index(t => t.SiteId)
                .Index(t => t.LandPlotTypeId);
            
            CreateTable(
                "dbo.LandPlotTypes",
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
            
            CreateTable(
                "dbo.SiteValuations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Description = c.String(),
                        GuidePrice = c.Decimal(precision: 18, scale: 2),
                        ProjectedGDV = c.Decimal(precision: 18, scale: 2),
                        ValuationDate = c.DateTime(),
                        ValuationBy = c.String(),
                        RTM = c.Boolean(nullable: false),
                        RTMPercent = c.Decimal(precision: 18, scale: 2),
                        RTMCommission = c.Decimal(precision: 18, scale: 2),
                        FinalPurchasePrice = c.Decimal(precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.PlanningApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        PlanningOfficeId = c.Int(nullable: false),
                        LandPlanningTypeId = c.Int(nullable: false),
                        PlanningDescription = c.String(),
                        ApplicationDate = c.DateTime(nullable: false),
                        ApplicationSubmitionDate = c.DateTime(),
                        CommitteeMeetingDate = c.DateTime(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlanningOffices", t => t.PlanningOfficeId, cascadeDelete: true)
                .ForeignKey("dbo.LandPlanningTypes", t => t.LandPlanningTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.PlanningOfficeId)
                .Index(t => t.LandPlanningTypeId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.PlanningOffices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        TownId = c.Int(),
                        PlanningContactId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.PlanningContactId)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .Index(t => t.PlanningContactId)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.LandPlanningTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlanningApprovals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanningApplicationId = c.Int(nullable: false),
                        LandPlanningApprovalStateTypeId = c.Int(),
                        ReferenceNumber = c.String(),
                        OfficerRecommendation = c.String(),
                        ApprovedDate = c.DateTime(),
                        RejectionDate = c.DateTime(),
                        AppealDate = c.DateTime(),
                        FullApprovalDate = c.DateTime(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LandPlanningApprovalStateTypes", t => t.LandPlanningApprovalStateTypeId)
                .ForeignKey("dbo.PlanningApplications", t => t.PlanningApplicationId, cascadeDelete: true)
                .Index(t => t.LandPlanningApprovalStateTypeId)
                .Index(t => t.PlanningApplicationId);
            
            CreateTable(
                "dbo.LandPlanningApprovalStateTypes",
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
            
            CreateTable(
                "dbo.PlanningAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileReference = c.String(),
                        SearchWords = c.String(),
                        PlanningApplicationId = c.Int(nullable: false),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlanningApplications", t => t.PlanningApplicationId, cascadeDelete: true)
                .Index(t => t.PlanningApplicationId);
            
            CreateTable(
                "dbo.SiteOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        OfferDate = c.DateTime(nullable: false),
                        OfferAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferUnits = c.Int(nullable: false),
                        OfferDecisionDate = c.DateTime(),
                        OfferNotes = c.String(),
                        DecissionTypeId = c.Int(),
                        ConditionalTypeId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConditionalTypes", t => t.ConditionalTypeId)
                .ForeignKey("dbo.DecissionTypes", t => t.DecissionTypeId)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.ConditionalTypeId)
                .Index(t => t.DecissionTypeId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.ConditionalTypes",
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
            
            CreateTable(
                "dbo.DecissionTypes",
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
            
            CreateTable(
                "dbo.SiteExchanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleStartDate = c.DateTime(),
                        AgreedSTCDate = c.DateTime(),
                        ProjectedExchangeDate = c.DateTime(),
                        ProjectedCompletionDate = c.DateTime(),
                        ActualExchangeDate = c.DateTime(),
                        ActualCompletionDate = c.DateTime(),
                        SearchesSumbitedDate = c.DateTime(),
                        PreliminaryEnquiryDate = c.DateTime(),
                        DraftContractSubmittedDate = c.DateTime(),
                        Commission = c.Double(),
                        NettFeePercent = c.Double(),
                        PurchaserId = c.Int(),
                        VendorSolicitorId = c.Int(),
                        PurchaserSolicitorId = c.Int(),
                        BrokerSolicitorId = c.Int(),
                        ProjectedReleaseDate = c.DateTime(),
                        SiteOfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.PurchaserId)
                .ForeignKey("dbo.Contacts", t => t.VendorSolicitorId)
                .ForeignKey("dbo.Contacts", t => t.PurchaserSolicitorId)
                .ForeignKey("dbo.Contacts", t => t.BrokerSolicitorId)
                .ForeignKey("dbo.SiteOffers", t => t.SiteOfferId, cascadeDelete: true)
                .Index(t => t.PurchaserId)
                .Index(t => t.VendorSolicitorId)
                .Index(t => t.PurchaserSolicitorId)
                .Index(t => t.BrokerSolicitorId)
                .Index(t => t.SiteOfferId);
            
            CreateTable(
                "dbo.SiteSaleOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Duration = c.Double(nullable: false),
                        Premium = c.Decimal(precision: 18, scale: 2),
                        MVPercent = c.Decimal(precision: 18, scale: 2),
                        Fee = c.Decimal(precision: 18, scale: 2),
                        Commission = c.Decimal(precision: 18, scale: 2),
                        OptionStartDate = c.DateTime(),
                        OptionExpiryDate = c.DateTime(),
                        ContractLongStopDate = c.DateTime(),
                        ProjectedLongStopDate = c.DateTime(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.SiteOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        UnitValueExVat = c.Double(nullable: false),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.LandCurrentuseTypes",
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
            
            CreateTable(
                "dbo.LandFieldTypes",
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
            
            CreateTable(
                "dbo.LandPriorityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandProbabilityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProbabilityIndex = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandProjectTypes",
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
            
            CreateTable(
                "dbo.LandStatusTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandSummaryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 50),
                        SortOrder = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        CorrespondanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Letters", t => t.CorrespondanceId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.CorrespondanceId);
            
            CreateTable(
                "dbo.Letters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        Document = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        ActionId = c.Int(),
                        DeliverTypeId = c.Int(),
                        TemplateId = c.Int(),
                        ContactId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Deliverby_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .ForeignKey("dbo.Actions", t => t.ActionId)
                .ForeignKey("dbo.DeliveryTypes", t => t.Deliverby_Id)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .Index(t => t.SiteId)
                .Index(t => t.ActionId)
                .Index(t => t.Deliverby_Id)
                .Index(t => t.TemplateId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlertDate = c.DateTime(nullable: false),
                        AlertComment = c.String(),
                        AlertActionTypeId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlertActionTypes", t => t.AlertActionTypeId)
                .Index(t => t.AlertActionTypeId);
            
            CreateTable(
                "dbo.AlertActionTypes",
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
            
            CreateTable(
                "dbo.DeliveryTypes",
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
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Markup = c.String(),
                        TemplateTypeId = c.Int(),
                        ChangedBy = c.String(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TemplateTypes", t => t.TemplateTypeId)
                .Index(t => t.TemplateTypeId);
            
            CreateTable(
                "dbo.TemplateTypes",
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
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Thread = c.String(nullable: false, maxLength: 255),
                        Level = c.String(nullable: false, maxLength: 50),
                        Logger = c.String(nullable: false, maxLength: 255),
                        Message = c.String(nullable: false, maxLength: 4000),
                        Exception = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Signature = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Templates", new[] { "TemplateTypeId" });
            DropIndex("dbo.Actions", new[] { "AlertActionTypeId" });
            DropIndex("dbo.Letters", new[] { "ContactId" });
            DropIndex("dbo.Letters", new[] { "TemplateId" });
            DropIndex("dbo.Letters", new[] { "Deliverby_Id" });
            DropIndex("dbo.Letters", new[] { "ActionId" });
            DropIndex("dbo.Letters", new[] { "SiteId" });
            DropIndex("dbo.Recipients", new[] { "CorrespondanceId" });
            DropIndex("dbo.Recipients", new[] { "ContactId" });
            DropIndex("dbo.SiteOrders", new[] { "SiteID" });
            DropIndex("dbo.SiteSaleOptions", new[] { "SiteId" });
            DropIndex("dbo.SiteExchanges", new[] { "SiteOfferId" });
            DropIndex("dbo.SiteExchanges", new[] { "BrokerSolicitorId" });
            DropIndex("dbo.SiteExchanges", new[] { "PurchaserSolicitorId" });
            DropIndex("dbo.SiteExchanges", new[] { "VendorSolicitorId" });
            DropIndex("dbo.SiteExchanges", new[] { "PurchaserId" });
            DropIndex("dbo.SiteOffers", new[] { "SiteId" });
            DropIndex("dbo.SiteOffers", new[] { "DecissionTypeId" });
            DropIndex("dbo.SiteOffers", new[] { "ConditionalTypeId" });
            DropIndex("dbo.PlanningAttachments", new[] { "PlanningApplicationId" });
            DropIndex("dbo.PlanningApprovals", new[] { "PlanningApplicationId" });
            DropIndex("dbo.PlanningApprovals", new[] { "LandPlanningApprovalStateTypeId" });
            DropIndex("dbo.PlanningOffices", new[] { "TownId" });
            DropIndex("dbo.PlanningOffices", new[] { "PlanningContactId" });
            DropIndex("dbo.PlanningApplications", new[] { "SiteId" });
            DropIndex("dbo.PlanningApplications", new[] { "LandPlanningTypeId" });
            DropIndex("dbo.PlanningApplications", new[] { "PlanningOfficeId" });
            DropIndex("dbo.SiteValuations", new[] { "SiteID" });
            DropIndex("dbo.Plots", new[] { "LandPlotTypeId" });
            DropIndex("dbo.Plots", new[] { "SiteId" });
            DropIndex("dbo.Contacts", new[] { "LandRoleTypeId" });
            DropIndex("dbo.Contacts", new[] { "TownId" });
            DropIndex("dbo.Sites", new[] { "LandSummaryTypeId" });
            DropIndex("dbo.Sites", new[] { "LandStatusTypeId" });
            DropIndex("dbo.Sites", new[] { "LandProjectTypeId" });
            DropIndex("dbo.Sites", new[] { "LandProbabilityTypeId" });
            DropIndex("dbo.Sites", new[] { "LandPriorityTypeId" });
            DropIndex("dbo.Sites", new[] { "LandFieldTypeId" });
            DropIndex("dbo.Sites", new[] { "LandCurrentuseTypeId" });
            DropIndex("dbo.Sites", new[] { "TownId" });
            DropIndex("dbo.Sites", new[] { "AgentId" });
            DropIndex("dbo.Sites", new[] { "OwnerId" });
            DropIndex("dbo.Sites", new[] { "NegotiatorId" });
            DropForeignKey("dbo.Templates", "TemplateTypeId", "dbo.TemplateTypes");
            DropForeignKey("dbo.Actions", "AlertActionTypeId", "dbo.AlertActionTypes");
            DropForeignKey("dbo.Letters", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Letters", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Letters", "Deliverby_Id", "dbo.DeliveryTypes");
            DropForeignKey("dbo.Letters", "ActionId", "dbo.Actions");
            DropForeignKey("dbo.Letters", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Recipients", "CorrespondanceId", "dbo.Letters");
            DropForeignKey("dbo.Recipients", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.SiteOrders", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.SiteSaleOptions", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SiteExchanges", "SiteOfferId", "dbo.SiteOffers");
            DropForeignKey("dbo.SiteExchanges", "BrokerSolicitorId", "dbo.Contacts");
            DropForeignKey("dbo.SiteExchanges", "PurchaserSolicitorId", "dbo.Contacts");
            DropForeignKey("dbo.SiteExchanges", "VendorSolicitorId", "dbo.Contacts");
            DropForeignKey("dbo.SiteExchanges", "PurchaserId", "dbo.Contacts");
            DropForeignKey("dbo.SiteOffers", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SiteOffers", "DecissionTypeId", "dbo.DecissionTypes");
            DropForeignKey("dbo.SiteOffers", "ConditionalTypeId", "dbo.ConditionalTypes");
            DropForeignKey("dbo.PlanningAttachments", "PlanningApplicationId", "dbo.PlanningApplications");
            DropForeignKey("dbo.PlanningApprovals", "PlanningApplicationId", "dbo.PlanningApplications");
            DropForeignKey("dbo.PlanningApprovals", "LandPlanningApprovalStateTypeId", "dbo.LandPlanningApprovalStateTypes");
            DropForeignKey("dbo.PlanningOffices", "TownId", "dbo.Towns");
            DropForeignKey("dbo.PlanningOffices", "PlanningContactId", "dbo.Contacts");
            DropForeignKey("dbo.PlanningApplications", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.PlanningApplications", "LandPlanningTypeId", "dbo.LandPlanningTypes");
            DropForeignKey("dbo.PlanningApplications", "PlanningOfficeId", "dbo.PlanningOffices");
            DropForeignKey("dbo.SiteValuations", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Plots", "LandPlotTypeId", "dbo.LandPlotTypes");
            DropForeignKey("dbo.Plots", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Contacts", "LandRoleTypeId", "dbo.LandRoleTypes");
            DropForeignKey("dbo.Contacts", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Sites", "LandSummaryTypeId", "dbo.LandSummaryTypes");
            DropForeignKey("dbo.Sites", "LandStatusTypeId", "dbo.LandStatusTypes");
            DropForeignKey("dbo.Sites", "LandProjectTypeId", "dbo.LandProjectTypes");
            DropForeignKey("dbo.Sites", "LandProbabilityTypeId", "dbo.LandProbabilityTypes");
            DropForeignKey("dbo.Sites", "LandPriorityTypeId", "dbo.LandPriorityTypes");
            DropForeignKey("dbo.Sites", "LandFieldTypeId", "dbo.LandFieldTypes");
            DropForeignKey("dbo.Sites", "LandCurrentuseTypeId", "dbo.LandCurrentuseTypes");
            DropForeignKey("dbo.Sites", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Sites", "AgentId", "dbo.Contacts");
            DropForeignKey("dbo.Sites", "OwnerId", "dbo.Contacts");
            DropForeignKey("dbo.Sites", "NegotiatorId", "dbo.Contacts");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Logs");
            DropTable("dbo.TemplateTypes");
            DropTable("dbo.Templates");
            DropTable("dbo.DeliveryTypes");
            DropTable("dbo.AlertActionTypes");
            DropTable("dbo.Actions");
            DropTable("dbo.Letters");
            DropTable("dbo.Recipients");
            DropTable("dbo.LandSummaryTypes");
            DropTable("dbo.LandStatusTypes");
            DropTable("dbo.LandProjectTypes");
            DropTable("dbo.LandProbabilityTypes");
            DropTable("dbo.LandPriorityTypes");
            DropTable("dbo.LandFieldTypes");
            DropTable("dbo.LandCurrentuseTypes");
            DropTable("dbo.SiteOrders");
            DropTable("dbo.SiteSaleOptions");
            DropTable("dbo.SiteExchanges");
            DropTable("dbo.DecissionTypes");
            DropTable("dbo.ConditionalTypes");
            DropTable("dbo.SiteOffers");
            DropTable("dbo.PlanningAttachments");
            DropTable("dbo.LandPlanningApprovalStateTypes");
            DropTable("dbo.PlanningApprovals");
            DropTable("dbo.LandPlanningTypes");
            DropTable("dbo.PlanningOffices");
            DropTable("dbo.PlanningApplications");
            DropTable("dbo.SiteValuations");
            DropTable("dbo.LandPlotTypes");
            DropTable("dbo.Plots");
            DropTable("dbo.LandRoleTypes");
            DropTable("dbo.Contacts");
            DropTable("dbo.Sites");
            DropTable("dbo.Towns");
        }
    }
}
