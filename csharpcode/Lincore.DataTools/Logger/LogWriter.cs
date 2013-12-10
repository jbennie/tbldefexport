using System;
using System.Collections.Generic;
using System.Text;

namespace Lincore.DataTools.Logger
{
    public class LogWriter
    {
        public static System.Text.StringBuilder abuilder; 

        public static void Write(string msg)
        {
           System.Console.WriteLine(msg);
           if (abuilder != null) abuilder.Append(msg); 
        }

        public static void Write(string msg, string src)
        {
           System.Console.WriteLine(src);
           System.Console.WriteLine(msg);

           if (abuilder != null)
           {
               abuilder.Append(src);
               abuilder.Append(msg);
           }
        }
    }
}
