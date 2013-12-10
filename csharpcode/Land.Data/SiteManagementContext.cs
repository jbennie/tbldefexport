using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using Land.Data.Models.Mapping;
using Land.Data.Account;
using Land.Data.Models;

namespace Land.Data
{
    public class SiteManagementContext : DbContext
    {

        static SiteManagementContext()
        {
            Database.SetInitializer<SiteManagementContext>(null);
        }

    
        public SiteManagementContext()
            : base("Name=LandContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Site.SiteConfiguration());
            modelBuilder.Configurations.Add(new SiteOffer.SiteOfferConfiguration());
        }

        // Domain data
        public DbSet<Town> Towns { get; set; }
        public DbSet<Site> Sites { get; set; }
        
        // Site Ref data
        public DbSet<LandFieldType> FieldTypes { get; set; }
        public DbSet<LandPriorityType> PriorityTypes { get; set; }
        public DbSet<LandProbabilityType> ProbabilityTypes { get; set; }
        public DbSet<LandProjectType> ProjectTypes { get; set; }
        public DbSet<LandRoleType> Roles { get; set; }
        public DbSet<LandStatusType> StatusTypes { get; set; }
        public DbSet<LandSummaryType> SummaryTypes { get; set; }
        public DbSet<LandCurrentuseType> CurrentUseTypes { get; set; }


        // Site and Sale Stages
        public DbSet<SiteOffer> Offers { get; set; }
            public DbSet<ConditionalType> ConditionalTypes { get; set; }
            public DbSet<DecissionType> DecissionTypes { get; set; }
            public DbSet<OfferSubjecttoType> OfferSubjecttoTypes { get;  set; }

        public DbSet<SiteOrder> Orders { get; set; }
        public DbSet<Plot> Plots { get; set; }
            public DbSet<LandPlotType> PlotTypes { get; set; }
        public DbSet<SiteValuation> Valuations { get; set; }
        public DbSet<SiteSaleOption> SaleOptions { get; set; }

        //CRM
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Negotiator> LandNegotiators { get; set; }
        public DbSet<Agent> LandAgents { get; set; }
        public DbSet<Owner> LandOwners { get; set; }
     //   public DbSet<BrokerSolicitor> BrokerSolicitors { get; set; }  //replaced with Buyer ..may reintroduce. 
        public DbSet<Purchaser> Purchasers { get; set; }
        public DbSet<PurchaserSolicitor> PurchaserSolicitors { get; set; }
        public DbSet<VendorSolicitor> VendorSolicitors { get; set; }
        public DbSet<Buyer> LandBuyer { get; set; }
        
        public DbSet<PlanningContact> PlanningContacts { get; set; }  
     
        // Planning
        public DbSet<PlanningApplication> PlanningApplications { get; set; }
      
        public DbSet<PlanningApproval> PlanningApprovals { get; set; }
        public DbSet<PlanningAttachment> PlanningAttachments { get; set; } 
        public DbSet<PlanningOffice> PlanningOffices { get; set; }
           
        public DbSet<LandPlanningType> PlanningTypes { get; set; }
        public DbSet<LandPlanningApprovalStateTypes> PlanningApprovalTypes { get; set; }
   
        // Correspondance (Greensheets, Memos, emails , Alerts
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<AlertActionType> AlertActionTypes { get; set; }
        public DbSet<TemplateType> TemplateTypes { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        

        //System 
        public DbSet<Log> Logs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<SiteExchange> SiteExchanges { get; set; }

        
    }
}
