using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMatrix.WebData.Resources;
using Land.Data.Attributes; 



namespace Land.Data.Models
{
    public class Contact: CrudInfo
    {
        public Contact(): base()
        {            
            Prefix = "";
            Initial = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Suffix = "";
            Title = "";
            IsStrategic = false;
            OrganisationName = "";
            Region = "";
            AddressLine1 = "";
            AddressLine2 = "";
            AddressLine3 = "";
            AddressLine4 = "";
            AddressCounty = "";
            AddressCountry = "UK"; 
            AddressPostcode = "";
            HomePhone = "";
            WorkPhone = "";
            MobilePhone = "";
            FaxNumber = "";
            AltPhone = "";
            Switchboard = "";
            DXNumber = "";
            DefaultEmail = "";
            AlternativeEmail = "";
            Website = "";
            DDIPhone = "";                   
        }


        public void Copyto(ref Contact to)
        {
            to.Prefix = this.Prefix;
            to.Initial = this.Initial;
            to.FirstName = this.FirstName;
            to.MiddleName = this.MiddleName;
            to.LastName = this.LastName;
            to.Suffix = this.Suffix;
            to.Title = this.Title;
            to.IsStrategic = this.IsStrategic;
            to.OrganisationName = this.OrganisationName;
            to.Region = this.Region;
            to.AddressLine1 = this.AddressLine1;
            to.AddressLine2 = this.AddressLine2;
            to.AddressLine3 = this.AddressLine3;
            to.AddressLine4 = this.AddressLine4;
            to.AddressCounty = this.AddressCounty;
            to.AddressPostcode = this.AddressPostcode;
            to.HomePhone = this.HomePhone;
            to.WorkPhone = this.WorkPhone;
            to.MobilePhone = this.MobilePhone;
            to.FaxNumber = this.FaxNumber;
            to.AltPhone = this.AltPhone;
            to.Switchboard = this.Switchboard;
            to.DXNumber = this.DXNumber;
            to.DefaultEmail = this.DefaultEmail;
            to.AlternativeEmail = this.AlternativeEmail;
            to.Website = this.Website;
            to.DDIPhone = this.DDIPhone;
            to.SetCreatedInfo();
            to.Town = this.Town;
            to.Type = this.Type; 
            to.SortOrder = this.Id; 
            to.referenceid = this.referenceid; 
       }
        
        public string Name { get { return FirstName + " " + LastName;  } }

        [Display(Name = "Full Name")]
        public string FullName { get { return Name;  } }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Code { get; set; }

    
        [Vtableimport("prefix")]
        public string Prefix { get; set; }
        
        [Display(Name = "Initial", AutoGenerateFilter = true)]
        [Vtableimport("intitial")]
        public string Initial { get; set; }

        [Display(Name = "Forname")]
        [Vtableimport("firstname")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [Vtableimport("middlename")]
        public string MiddleName { get; set; }

        [Display(Name = "Surname")]
        [Vtableimport("lastname")]
        public string LastName { get; set; }
        [Vtableimport("suffix")]
        public string Suffix { get; set; }
        [Vtableimport("title")]
        public string Title { get; set; }
         
        [Display(Name = "Is Strategic", AutoGenerateFilter = true)]
        [Vtableimport("isstrategic")]
        public bool IsStrategic { get; set; }        
        [Display(Name = "Organisation Name", AutoGenerateFilter = true)]        
        [Vtableimport("organisationname")]
        public string OrganisationName { get; set; }        
        [Vtableimport("region")]
        public string Region { get; set; }        
        [Vtableimport("addressline1")]
        public string AddressLine1 { get; set; }        
        [Vtableimport("addressline2")]
        public string AddressLine2 { get; set; }
        [Vtableimport("addressline3")]
        public string AddressLine3 { get; set; }
        [Vtableimport("addressline4")]
        public string AddressLine4 { get; set; }         
        [Display(Name = "County", AutoGenerateFilter = true)]
        [Vtableimport("county")]
        public string AddressCounty { get; set; }        
        [Display(Name = "Postcode", AutoGenerateFilter = true)]
        [Vtableimport("postcode")]
        public string AddressPostcode { get; set; }

        [Display(Name = "Country", AutoGenerateFilter = true)]
        [Vtableimport("country")]
        public string AddressCountry { get; set; }

        [Display(Name = "Home Phone")]
        [Vtableimport("homephone")]
        public string HomePhone { get; set; }

        [Display(Name = "Work Phone")]
        [Vtableimport("workphone")]
        public string WorkPhone { get; set; }

        [Display(Name = "Mobile Phone")]
        [Vtableimport("mobilephone")]
        public string MobilePhone { get; set; }

        [Display(Name = "Fax")]
        [Vtableimport("faxnumber")]
        public string FaxNumber { get; set; }

        [Display(Name = "Alt phone")]
        [Vtableimport("altphone")]
        public string AltPhone { get; set; }

        
        [Vtableimport("switchboard")]
        public string Switchboard { get; set; }

        [Display(Name = "DX Number")]
        [Vtableimport("dxnumber")]
        public string DXNumber { get; set; }

        [Display(Name = "Email")]
        [Vtableimport("defaultemail")]
        public string DefaultEmail { get; set; }

        [Display(Name = "Alt Email")]
        [Vtableimport("altemail")]
        public string AlternativeEmail { get; set; }

        [Vtableimport("website")]
        public string Website { get; set; }
        
        [Display(Name = "Extension")]
        [Vtableimport("ddiphone")]
        public string DDIPhone { get; set; }

        public Nullable<int> referenceid { get; set; } // use to hold historical id values

        public Nullable<int> SortOrder { get; set; } 

        public Nullable<int> LandRoleTypeId { get; set; }
        public Nullable<int> TownId { get; set; }
      
        public Town Town { get; set; }
        public LandRoleType Type { get; set; }



        public static string GetCode(Contact c) 
        {
            return (c.Prefix + " "+c.FirstName + " " + c.LastName + " " +c.Title +" "+ c.OrganisationName).Trim(); 
        }
        public int StoredScore = 0;
     
        /// <summary>
        /// Calculate the generic score
        /// </summary>
        /// <param name="filterstr"></param>
        /// <returns></returns>
        public int CalcScore(ref string filterstr) 
        {
            int score = 0;

            score += DataUtilities.getscore(Name, ref filterstr);
            score += DataUtilities.getscore(Initial, ref filterstr);
            score += DataUtilities.getscore(OrganisationName, ref filterstr);            
            score += DataUtilities.getscore(AddressLine1, ref filterstr);
            score += DataUtilities.getscore(AddressLine2, ref filterstr);
            score += DataUtilities.getscore(AddressLine3, ref filterstr);
            score += DataUtilities.getscore(AddressPostcode, ref filterstr);
            score += DataUtilities.getscore(AddressCounty, ref filterstr);
            score += DataUtilities.getscore(Region, ref filterstr); 

            if (Town != null)
            {
                score += Town.CalcScore(ref filterstr);
            }

            if (Type != null)
            {
                score += Type.CalcScore(ref filterstr);
            }
            StoredScore = score; 
            return score; 
        }

        /// <summary>
        /// Find reasons to include and item in the result set
        /// if a valid string is passed, include items that do match the string. 
        /// if and invalid string is passed, include all results. 
        /// </summary>
        /// <param name="filterstr"></param>
        /// <returns></returns>
        public bool FilterIn(string infilterstr)
        {
        
            if (string.IsNullOrEmpty(infilterstr)) return true;
    
            string filterstr = infilterstr.ToLower(); 
            int score = 0;

            score += CalcScore(ref filterstr);
         

            // extended Scores 
            score += DataUtilities.getscore(Id.ToString(), ref filterstr);
           
            if (score >= 1)
            {
                return true;
            }

            return false;
        }
    }
}
