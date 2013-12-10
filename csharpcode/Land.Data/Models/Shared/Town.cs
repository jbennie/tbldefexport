using System;
using System.Collections.Generic;

namespace Land.Data.Models
{
    public class Town:CrudInfo
    {
        public Town()
        {
            Name = "New Town";
            Postcode = "";
            Country = "UK";
            County = ""; 
            Duplicate = false;
            Latitude = "0";
            Longditude = "0";
        }

        public int Id { get; set; }        
        public string Name { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longditude { get; set; }
        public bool Duplicate { get; set; }
        
        public int referenceId { get; set; }  // stores historical ID. 

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
            // Extended Score
          
            if (score >= 1)
            {
                return true;
            }

            return false;
        }
    }
}
