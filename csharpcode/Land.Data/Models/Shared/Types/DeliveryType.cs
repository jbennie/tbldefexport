using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    /// <summary>
    /// encodes the distinct send types i.e. email, post, other
    /// </summary>
    public class DeliveryType:LandUnitType
    {
        public DeliveryType()
        {
        }
    }
}
