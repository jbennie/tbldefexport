using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Land.Data.Models
{
    public abstract class LandUnitType
    {
        public LandUnitType()
        {
        }

        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage="Name cannot be greater than 50 Characters.")]
        public string Name { get; set; }

        [StringLength(3, ErrorMessage = "Name cannot be greater than 3 characters.")]
        public string Code { get; set; }
               
        public Nullable<int> SortOrder { get; set; }

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

            // extended Scores 
            score += DataUtilities.getscore(Id.ToString(), ref filterstr);

            if (score >= 1)
            {
                return true;
            }

            return false;
        }


        public void SetCreatedInfo()
        {
          
        }

        public void UpdatedCreatedInfo()
        {
            
        }
    }
}
