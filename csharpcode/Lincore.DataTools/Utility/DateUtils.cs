using System;
using System.Collections;

namespace Lincore.DataTools.Utility
{
	/// <summary>
	/// Summary description for Common.
	/// </summary>
	public abstract class DateUtils
	{
		public DateUtils()
		{
		}

		#region date functions to convert CCYYMMDD and YYMMDD formats 


		/// <summary>
		/// Converts a DateTime object to a string in CCYYMMDD format
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static string DTEtoCCYYMMDD(DateTime d)
		{
			return d.Year.ToString() + d.Month.ToString().PadLeft(2,'0') + d.Day.ToString().PadLeft(2,'0'); 
		}


		/// <summary>
		/// Mimics the PMS Format 
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static string DTEtoPMS_DD_MMM_CCYY(DateTime d)
		{			
			return d.Day.ToString().PadLeft(2,'0') + "-"+ MonthtoShrtString(d.Month) +"-"+d.Year.ToString();
		}

		/// <summary>
		/// Converts a DateTime object to a string in CCYYMMDD format
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static string DTEtoCCYYMMDD_HHMM(DateTime d)
		{
			return d.Year.ToString() + d.Month.ToString().PadLeft(2,'0') + d.Day.ToString().PadLeft(2,'0')+"_"+d.Hour.ToString().PadLeft(2,'0')+ d.Minute.ToString().PadLeft(2,'0'); 
		}
		public static string DTEtoCCYYMMDD_HHMMSS(DateTime d)
		{
			return d.Year.ToString() + d.Month.ToString().PadLeft(2,'0') + d.Day.ToString().PadLeft(2,'0')+"_"+d.Hour.ToString().PadLeft(2,'0')+ d.Minute.ToString().PadLeft(2,'0')+ d.Second.ToString().PadLeft(2,'0'); 
		}



		/// <summary>
		/// Converts a DateTime object to a string in CCYYMMDD format
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static string DTEtoCCYYMM(DateTime d)
		{
			return d.Year.ToString() + d.Month.ToString().PadLeft(2,'0'); 
		}

		/// <summary>
		/// converts a string date in the formats CCYYMMDD or YYMMDD or CCYYMM  to a DateTime object 
		/// if a string in the format YYMMDD format the century will be determine as 1900 if the YY is in the range ((year >=70) && (year <= 99))
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static DateTime YYMMDDtoDTE(string s)
		{
			int year = 0; 
			int month = 0; 
			int day = 0; 


			if (s.Length == 6)
			{
				year = Convert.ToInt32(s.Substring(0,4)); 
					//assume in format  YYMMDD
					if ((year >=70) && (year <= 99))
					{
						year = Convert.ToInt32("19" + s.Substring(0,2)); 
					}
					else 
					{   
						year = Convert.ToInt32("20" + s.Substring(0,2));
					}

					month = Convert.ToInt32(s.Substring(2,2)); 
					day = Convert.ToInt32(s.Substring(4,2)); 
			}
			else 
			{
				throw new Exception ("invalid date format"); 
			}

			return new DateTime (year,month,day); 
		}	
	

		public static DateTime CCYYMMDDtoDTE(string s)
		{
			int year = 0; 
			int month = 0; 
			int day = 0; 

			if (s.Length == 8)
			{
				year = Convert.ToInt32(s.Substring(0,4)); 
				month = Convert.ToInt32(s.Substring(4,2)); 
				day = Convert.ToInt32(s.Substring(6,2)); 
			
			}
			else 
			{
				throw new Exception ("invalid date format"); 
			}

			if ((year == 0 )||(month == 0) || (day==0)) return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)); 

			return new DateTime (year,month,day); 
		}	

		public static DateTime CCYYMMtoDTE(string s)
		{
			int year = 0; 
			int month = 0; 
			int day = 0; 
			//	 if CCYYMM
			if (s.Length == 6)
			{
				year = Convert.ToInt32(s.Substring(0,4)); 
				month = Convert.ToInt32(s.Substring(4,2)); 
				day = DateTime.DaysInMonth(year,month); 
			}
			else 
			{
			  throw new Exception ("invalid date format"); 
			}

			return new DateTime (year,month,day);
		}
		#endregion


		#region Date function to convert to day Month year i.e. 1 December 2003

		/// <summary>
		/// </summary>
		/// <param name="DTE"></param>
		/// <returns></returns>
		public static string ToISOLongDate(DateTime DTE)
		{
			return DTE.Day.ToString() +" " + MonthtoString(DTE.Month) + " " + DTE.Year; 
		}

		private static string MonthtoString(Int32 monthnum)
		{
			switch (monthnum)
			{
				case 1: return "January";
				case 2: return "February";
				case 3: return "March";
				case 4: return "April";
				case 5: return "May";
				case 6: return "June";
				case 7: return "July";
				case 8: return "August";
				case 9: return "September";
				case 10: return "October";
				case 11: return "November";
				case 12: return "December";
				default : return "Not a Month"; 
			}
		}

		private static string MonthtoShrtString(Int32 monthnum)
		{
			switch (monthnum)
			{
				case 1: return "Jan";
				case 2: return "Feb";
				case 3: return "Mar";
				case 4: return "Apr";
				case 5: return "May";
				case 6: return "Jun";
				case 7: return "Jul";
				case 8: return "Aug";
				case 9: return "Sep";
				case 10: return "Oct";
				case 11: return "Nov";
				case 12: return "Dec";
				default : return "Not a Month"; 
			}
		}

		private static Int32 Mthname2Int32(string s)
		{
			switch (s.Substring(0,3).ToLower())
			{
				case "jan": return 1;
				case "feb": return 2;
				case "mar": return 3;
				case "apr": return 4;
				case "may":return 5;
				case "jun": return 6;
				case "jul": return 7;
				case "aug":return 8;
				case "sep": return 9;
				case "oct": return 10;
				case "nov": return 11;
				case "dec": return 12;

				default : return 0; 

			}
		}

		/// <summary>
		/// Convert date in format dd-MMM-YYYY	e.g. "05-Dec-2005"
		/// </summary>
		/// <param name="PMSfmt"></param>
		/// <returns></returns>
		public static DateTime DDMMMCCYY2DTE(string PMSfmt)
		{
			int day = Convert.ToInt32( PMSfmt.Substring(0,2)); 
			int year = Convert.ToInt32(PMSfmt.Substring(7,4)); 
			int month = Mthname2Int32(PMSfmt.Substring(3,3)); 


			return new DateTime(year, month, day); 
		}


		#endregion 

		/// <summary>
		/// Returns a List of MonthEnds inclusive of a start and end Date. 
		/// </summary>
		/// <param name="Min"></param>
		/// <param name="Max"></param>
		/// <returns></returns>
		public static ArrayList GetMthEnds(DateTime Min, DateTime Max)
		{
			ArrayList tmp = new ArrayList(); 

			DateTime adte = new DateTime(Min.Ticks); 
			tmp.Add(new DateTime(Min.Ticks)); 

			do 
			{
				adte = adte.AddMonths(1); 
				adte = new DateTime(adte.Year, adte.Month, DateTime.DaysInMonth(adte.Year, adte.Month)); 

				if (adte <= Max)
				tmp.Add(adte);
			}

			while( adte <= Max);
		//	tmp.Add(new DateTime(Max.Ticks)); 

			return tmp; 
		}

		/// <summary>
		/// Returns a List of MonthEnds inclusive of a start and end Date. 
		/// </summary>
		/// <param name="Min"></param>
		/// <param name="Max"></param>
		/// <returns></returns>
		public static int GetMthEndCount(DateTime Min, DateTime Max)
		{
			ArrayList tmp = new ArrayList(); 

			DateTime adte = new DateTime(Min.Ticks); 
			tmp.Add(new DateTime(Min.Ticks)); 

			do 
			{
				adte = adte.AddMonths(1); 
				adte = new DateTime(adte.Year, adte.Month, DateTime.DaysInMonth(adte.Year, adte.Month)); 

				if (adte <= Max)
					tmp.Add(adte);
			}

			while( adte <= Max);
			//	tmp.Add(new DateTime(Max.Ticks)); 

			return tmp.Count; 
		}

		/// <summary>
		/// Returns a List of MonthEnds 
		/// </summary>
		/// <param name="d">date of period end </param>
		/// <param name="regress">number of months to regress</param>
		/// <returns></returns>
		public static ArrayList GetMthEnds(DateTime d, int regress)
		{
			ArrayList tmp = new ArrayList(regress); 

			DateTime Lastdte = new DateTime(d.Year, d.Month,  DateTime.DaysInMonth(d.Year, d.Month));			
			tmp.Add(Lastdte);  

			int year = 0; 
			int month = 0; 
			int day = 0; 

			for(int i = 0; i <= regress-1; i ++)
			{
				if (Lastdte.Month == 1)
				{
					month = 12; 
					year = (Lastdte.Year -1); 
				}
				else 
				{
					month = Lastdte.Month -1; 
					year = Lastdte.Year;
				}

				day = DateTime.DaysInMonth(year,month); 

				Lastdte = new DateTime(year,month,day); 

				tmp.Add(Lastdte);
			} 

			return tmp; 
		}

		/// <summary>
		/// Convert date supplied to the relative month end 
		/// </summary>
		/// <param name="d">the relative date i.e. DateTime.now</param>
		/// <returns></returns>
		public static DateTime GetCurrentMthEnd(DateTime d)
		{
			Int32 y = d.Year; 
			Int32 m = d.Month;

			if (m != 1)
			{
				m--; 
			}
			else if (m == 1)
			{
				m=12; 
				y--; 
			}
			return new DateTime(y,m , DateTime.DaysInMonth(y,m)); 
		}

		public static DateTime GetCurrentQtrEnd(DateTime d)
		{
			Int32 y = d.Year; 
			Int32 m = d.Month;

			switch (m)
			{
				case 1: m = 12; y--; break;
				case 2:	m = 12; y--; break;
				//case 3: 
				case 4: m = 3; break;
				case 5:	m = 3;  break;
				//case 6:
				case 7:	m = 6; break;
				case 8:	m = 6; break;
				//case 9:
				case 10: m = 9;	break;
				case 11: m = 9;	break;
				//case 12: 
			}

			return new DateTime(y,m , DateTime.DaysInMonth(y,m)); 
		}

		/// <summary>
		/// works out the next Q end 
		/// i.e if 31/12/2006 returns 31/3/2007
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static DateTime IncOneQtrEnd(DateTime d)
		{
			
			Int32 y = d.Year; 
			Int32 m = d.Month;

			switch (m)
			{
				case 12: m = 3; y++; break; 
				case 1: m = 3; break;
				case 2:	m = 3;  break;

				case 3: m = 6; break;
				case 4: m = 6; break;
				case 5:	m = 6; break;
				
				case 6: m = 9; break;
				case 7:	m = 9; break;
				case 8:	m = 9; break;
				
				case 9: m = 12; break;
				case 10: m = 12;	break;
				case 11: m = 12;	break;
			}

			return new DateTime(y,m , DateTime.DaysInMonth(y,m)); 
		}

		public static DateTime GetPrevMthEnd(DateTime d)
		{
			ArrayList tmp = GetMthEnds(d,2); 
			return (DateTime)tmp[1]; 
		}

		public static DateTime GetPrevQtrEnd(DateTime d)
		{
			ArrayList tmp = GetMthEnds(d,3); 
			return (DateTime)tmp[3]; 
		}

		public static DateTime GetPrevDECEnd(DateTime d)
		{
		
			int y = d.Year; 
			y = y - 1; 

			return new DateTime(y,12,31); 
			//ArrayList tmp = GetMthEnds(d,2); 
			//return (DateTime)tmp[1]; 
		}


		public static DateTime GetNextMthEnd(DateTime d)
		{

			Int32 Month = d.Month; 
			Int32 Year = d.Year; 

			if (Month ==12)
			{
				Month = 1; 
				Year = Year +1; 
			}
			else
			{
				Month = Month +1; 
			}

			return new DateTime(Year, Month, DateTime.DaysInMonth(Year,Month)); 
		}
	}
}
