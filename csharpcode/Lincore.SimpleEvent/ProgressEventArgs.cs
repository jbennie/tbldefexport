using System;

namespace Lincore.SimpleEvent
{
	/// <summary>
	/// Summary description for ProgressEventArgs.
	/// </summary>
	public class ProgressEventArgs: EventArgs
	{
		public int TotalSteps = 100; 
		public int StepSize = 1; 
		public int Steps =0; 

		public ProgressEventArgs(int aStep)
		{
			Steps = aStep; 
		}

		public ProgressEventArgs(int aStep, int Size, int total)
		{
			TotalSteps = total; 
			StepSize = Steps; 
			Steps = aStep; 
		}
	}
}
