using System;

namespace Lincore.SimpleEvent
{
	public class RptEventArgs: EventArgs
	{
		public string _Message; 

		public RptEventArgs(string Message):base() 
		{
			_Message = Message; 
			
		}
	
	}
}
