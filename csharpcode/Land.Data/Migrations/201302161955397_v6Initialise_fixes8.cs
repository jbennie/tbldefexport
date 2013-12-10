namespace Land.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6Initialise_fixes8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanningApplications", "PlanningOfficeId", "dbo.PlanningOffices");
            DropForeignKey("dbo.PlanningApplications", "LandPlanningTypeId", "dbo.LandPlanningTypes");
            DropIndex("dbo.PlanningApplications", new[] { "PlanningOfficeId" });
            DropIndex("dbo.PlanningApplications", new[] { "LandPlanningTypeId" });
            AlterColumn("dbo.PlanningApplications", "PlanningOfficeId", c => c.Int());
            AlterColumn("dbo.PlanningApplications", "LandPlanningTypeId", c => c.Int());
            AddForeignKey("dbo.PlanningApplications", "PlanningOfficeId", "dbo.PlanningOffices", "Id");
            AddForeignKey("dbo.PlanningApplications", "LandPlanningTypeId", "dbo.LandPlanningTypes", "Id");
            CreateIndex("dbo.PlanningApplications", "PlanningOfficeId");
            CreateIndex("dbo.PlanningApplications", "LandPlanningTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlanningApplications", new[] { "LandPlanningTypeId" });
            DropIndex("dbo.PlanningApplications", new[] { "PlanningOfficeId" });
            DropForeignKey("dbo.PlanningApplications", "LandPlanningTypeId", "dbo.LandPlanningTypes");
            DropForeignKey("dbo.PlanningApplications", "PlanningOfficeId", "dbo.PlanningOffices");
            AlterColumn("dbo.PlanningApplications", "LandPlanningTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.PlanningApplications", "PlanningOfficeId", c => c.Int(nullable: false));
            CreateIndex("dbo.PlanningApplications", "LandPlanningTypeId");
            CreateIndex("dbo.PlanningApplications", "PlanningOfficeId");
            AddForeignKey("dbo.PlanningApplications", "LandPlanningTypeId", "dbo.LandPlanningTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlanningApplications", "PlanningOfficeId", "dbo.PlanningOffices", "Id", cascadeDelete: true);
        }
    }
}
