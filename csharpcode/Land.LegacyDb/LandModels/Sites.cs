using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWH_LAND.BusinessObjects
{


    public enum EGreenFieldType { Brownfield, Greenfield};
    public enum ETypeofSite { Strategic, Immediate};
    public enum EHasPlanning { Yes , No, Unknown, Refused};
    public enum EProjectedPredominatUnits { Flats, Houses};
    public enum ERWHBuyer { RWH, MH};
   

    public class Sites
    {

        public Int32 ID = -1; 

        public string Address1;
        public string Address2;
        public string Address3;
        public string Town;
        public string County;
        public string Postcode;

        public DateTime DateReceivied;
        
        public String OriginType;
        public String BuyerType;
        public String StateType;

        public String DeadFileNr;
        public String DeadFileBoxNr;

        #region PlotSize
        private Double _sqm = 1;

        /*
         * sqm = Acr * 4046.86
         * sqft = Acr * 43560
         * Acr = hect *2.471         
         */

        public double SqM
        {
            get { return _sqm; }
            set { _sqm = value; }
        }
        public double SqFT
        {
            get { return _sqm / 4046.86 * 43560; }
            set
            {
                SqM = (value * 4046.86 / 43560);
            }
        }
        public double Hectares
        {
            get
            {
                return (_sqm / 2.471 / 4046.86);
            }
            set
            {
                SqM = (value * 2.471 * 4046.86);
            }
        }
        public double Acres
        {
            get
            {
                return (_sqm / 4046.86);
            }
            set
            {
                SqM = (value * 4046.86);
            }
        }
        #endregion

        public String ProjProdUnit;
        public String Plots;
        public String GreenFieldType;
        public String Summary;
        public String SummaryDetail;
        public String UseOfLand;
        public String GuidePrice;
        public String ProjGDV;
        public double RTM_Percent;
        public double RTC_Commission; 
    }



}
