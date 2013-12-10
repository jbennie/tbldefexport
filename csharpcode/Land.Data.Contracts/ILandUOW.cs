using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincore.GenericInterfaces.EntityFramework; 
using Land.Data.Models; 

namespace Land.Data.Contracts
{
    public interface ILandUOW : IUOW
    {

    // ISitesRepository Sites {get; }

     IRepository<Site> Sites { get; }
     IRepository<Plot> Plots { get; }
   //  IRepository<Contact> Contacts { get; }
     IRepository<Contact> Contacts { get; }

     IRepository<Owner> Owners { get; }
     IRepository<Negotiator> Negotiators { get; }
     IRepository<Agent> Agents { get; }

     IRepository<Town> Towns { get;}
     IRepository<LandFieldType> FieldTypes { get; }
     IRepository<LandPriorityType> PriorityTypes { get; }
     IRepository<LandProjectType> ProjectTypes { get; }
     IRepository<LandRoleType> Roles { get; }
     IRepository<LandStatusType> StatusTypes { get; }
     IRepository<LandProbabilityType> ProbabilityTypes { get; }

     IRepository<SiteOffer> Offers { get; }
     IRepository<SiteValuation> Valuations { get; }
     IRepository<SiteSaleOption> Options { get; }

    }
}
