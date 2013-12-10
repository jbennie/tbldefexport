using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class PaymentReceipt
    {
        public PaymentReceipt()
        {
        }

        public int Id { get; set; }
        public int AccountId {get; set;}

        public System.DateTime DatePaid { get; set; }
        public double ReceiptValue { get; set; }
        public string ReceiptConfirmedBy { get; set; }
        public Nullable<System.DateTime> ReceiptConfirmedOn { get; set; }
    
        public virtual Account Account{get; set;}

    }
}
