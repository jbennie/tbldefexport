using System;

namespace Land.Data.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class Vtableimport : Attribute
    {
        public Vtableimport(string name)
        {
            this.Fieldname = name; 
        }

        public string Fieldname; 
    }
}
