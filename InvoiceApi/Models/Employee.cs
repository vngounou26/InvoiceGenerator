﻿using System;
using System.Collections.Generic;

namespace InvoiceApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Title { get; set; }
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? HomePhone { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
