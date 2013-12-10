namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandRoleTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandPlotTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandPlanningApprovalStateTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.ConditionalTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.DecissionTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.OfferSubjecttoTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandCurrentuseTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandFieldTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandPriorityTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandProbabilityTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.LandProjectTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.AlertActionTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.DeliveryTypes", "Code", c => c.String(maxLength: 3));
            AddColumn("dbo.TemplateTypes", "Code", c => c.String(maxLength: 3));
            AlterColumn("dbo.LandPlanningTypes", "Code", c => c.String(maxLength: 3));
            DropColumn("dbo.LandRoleTypes", "ChangedBy");
            DropColumn("dbo.LandRoleTypes", "CreatedBy");
            DropColumn("dbo.LandRoleTypes", "Modified");
            DropColumn("dbo.LandRoleTypes", "Created");
            DropColumn("dbo.LandPlotTypes", "ChangedBy");
            DropColumn("dbo.LandPlotTypes", "CreatedBy");
            DropColumn("dbo.LandPlotTypes", "Modified");
            DropColumn("dbo.LandPlotTypes", "Created");
            DropColumn("dbo.LandPlanningTypes", "ChangedBy");
            DropColumn("dbo.LandPlanningTypes", "CreatedBy");
            DropColumn("dbo.LandPlanningTypes", "Modified");
            DropColumn("dbo.LandPlanningTypes", "Created");
            DropColumn("dbo.LandPlanningApprovalStateTypes", "ChangedBy");
            DropColumn("dbo.LandPlanningApprovalStateTypes", "CreatedBy");
            DropColumn("dbo.LandPlanningApprovalStateTypes", "Modified");
            DropColumn("dbo.LandPlanningApprovalStateTypes", "Created");
            DropColumn("dbo.ConditionalTypes", "ChangedBy");
            DropColumn("dbo.ConditionalTypes", "CreatedBy");
            DropColumn("dbo.ConditionalTypes", "Modified");
            DropColumn("dbo.ConditionalTypes", "Created");
            DropColumn("dbo.DecissionTypes", "ChangedBy");
            DropColumn("dbo.DecissionTypes", "CreatedBy");
            DropColumn("dbo.DecissionTypes", "Modified");
            DropColumn("dbo.DecissionTypes", "Created");
            DropColumn("dbo.OfferSubjecttoTypes", "ChangedBy");
            DropColumn("dbo.OfferSubjecttoTypes", "CreatedBy");
            DropColumn("dbo.OfferSubjecttoTypes", "Modified");
            DropColumn("dbo.OfferSubjecttoTypes", "Created");
            DropColumn("dbo.LandCurrentuseTypes", "ChangedBy");
            DropColumn("dbo.LandCurrentuseTypes", "CreatedBy");
            DropColumn("dbo.LandCurrentuseTypes", "Modified");
            DropColumn("dbo.LandCurrentuseTypes", "Created");
            DropColumn("dbo.LandFieldTypes", "ChangedBy");
            DropColumn("dbo.LandFieldTypes", "CreatedBy");
            DropColumn("dbo.LandFieldTypes", "Modified");
            DropColumn("dbo.LandFieldTypes", "Created");
            DropColumn("dbo.LandPriorityTypes", "Priority");
            DropColumn("dbo.LandPriorityTypes", "ChangedBy");
            DropColumn("dbo.LandPriorityTypes", "CreatedBy");
            DropColumn("dbo.LandPriorityTypes", "Modified");
            DropColumn("dbo.LandPriorityTypes", "Created");
            DropColumn("dbo.LandProbabilityTypes", "ProbabilityIndex");
            DropColumn("dbo.LandProbabilityTypes", "ChangedBy");
            DropColumn("dbo.LandProbabilityTypes", "CreatedBy");
            DropColumn("dbo.LandProbabilityTypes", "Modified");
            DropColumn("dbo.LandProbabilityTypes", "Created");
            DropColumn("dbo.LandProjectTypes", "ChangedBy");
            DropColumn("dbo.LandProjectTypes", "CreatedBy");
            DropColumn("dbo.LandProjectTypes", "Modified");
            DropColumn("dbo.LandProjectTypes", "Created");
            DropColumn("dbo.LandStatusTypes", "ChangedBy");
            DropColumn("dbo.LandStatusTypes", "CreatedBy");
            DropColumn("dbo.LandStatusTypes", "Modified");
            DropColumn("dbo.LandStatusTypes", "Created");
            DropColumn("dbo.LandSummaryTypes", "ChangedBy");
            DropColumn("dbo.LandSummaryTypes", "CreatedBy");
            DropColumn("dbo.LandSummaryTypes", "Modified");
            DropColumn("dbo.LandSummaryTypes", "Created");
            DropColumn("dbo.AlertActionTypes", "ChangedBy");
            DropColumn("dbo.AlertActionTypes", "CreatedBy");
            DropColumn("dbo.AlertActionTypes", "Modified");
            DropColumn("dbo.AlertActionTypes", "Created");
            DropColumn("dbo.DeliveryTypes", "ChangedBy");
            DropColumn("dbo.DeliveryTypes", "CreatedBy");
            DropColumn("dbo.DeliveryTypes", "Modified");
            DropColumn("dbo.DeliveryTypes", "Created");
            DropColumn("dbo.TemplateTypes", "ChangedBy");
            DropColumn("dbo.TemplateTypes", "CreatedBy");
            DropColumn("dbo.TemplateTypes", "Modified");
            DropColumn("dbo.TemplateTypes", "Created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TemplateTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.TemplateTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.TemplateTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.TemplateTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.DeliveryTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.DeliveryTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.AlertActionTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.AlertActionTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.AlertActionTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.AlertActionTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandSummaryTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandSummaryTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandSummaryTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandSummaryTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandStatusTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandStatusTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandStatusTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandStatusTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandProjectTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandProjectTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandProjectTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandProjectTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandProbabilityTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandProbabilityTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandProbabilityTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandProbabilityTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandProbabilityTypes", "ProbabilityIndex", c => c.Int());
            AddColumn("dbo.LandPriorityTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPriorityTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPriorityTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandPriorityTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandPriorityTypes", "Priority", c => c.Int());
            AddColumn("dbo.LandFieldTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandFieldTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandFieldTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandFieldTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandCurrentuseTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandCurrentuseTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandCurrentuseTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandCurrentuseTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.OfferSubjecttoTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.OfferSubjecttoTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.OfferSubjecttoTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.OfferSubjecttoTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.DecissionTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.DecissionTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.DecissionTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.DecissionTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.ConditionalTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ConditionalTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.ConditionalTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.ConditionalTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandPlanningApprovalStateTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlanningApprovalStateTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlanningApprovalStateTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandPlanningApprovalStateTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandPlanningTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlanningTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlanningTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandPlanningTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandPlotTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlotTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandPlotTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandPlotTypes", "ChangedBy", c => c.String());
            AddColumn("dbo.LandRoleTypes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandRoleTypes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.LandRoleTypes", "CreatedBy", c => c.String());
            AddColumn("dbo.LandRoleTypes", "ChangedBy", c => c.String());
            AlterColumn("dbo.LandPlanningTypes", "Code", c => c.String(nullable: false, maxLength: 3));
            DropColumn("dbo.TemplateTypes", "Code");
            DropColumn("dbo.DeliveryTypes", "Code");
            DropColumn("dbo.AlertActionTypes", "Code");
            DropColumn("dbo.LandProjectTypes", "Code");
            DropColumn("dbo.LandProbabilityTypes", "Code");
            DropColumn("dbo.LandPriorityTypes", "Code");
            DropColumn("dbo.LandFieldTypes", "Code");
            DropColumn("dbo.LandCurrentuseTypes", "Code");
            DropColumn("dbo.OfferSubjecttoTypes", "Code");
            DropColumn("dbo.DecissionTypes", "Code");
            DropColumn("dbo.ConditionalTypes", "Code");
            DropColumn("dbo.LandPlanningApprovalStateTypes", "Code");
            DropColumn("dbo.LandPlotTypes", "Code");
            DropColumn("dbo.LandRoleTypes", "Code");
        }
    }
}