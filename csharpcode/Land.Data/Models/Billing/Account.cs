using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Account: CrudInfo
    {
        public Account()
        {
            IsActive = true;
            AccountReference = "";
            Invoices = new List<Invoice>();
            PaymentReceipts = new List<PaymentReceipt>();
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public string AccountReference { get; set; }
        public bool IsActive { get; set; }

        public Contact Contact { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<PaymentReceipt> PaymentReceipts { get; set; }
     
    }
}
