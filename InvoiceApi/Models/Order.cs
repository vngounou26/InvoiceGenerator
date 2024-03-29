﻿using System;
using System.Collections.Generic;

namespace InvoiceApi.Models
{
    public partial class Order
    {
        public Order()
        {
            Factures = new HashSet<Facture>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int? Validate { get; set; }
        public int? Sell { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<Facture>? Factures { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
