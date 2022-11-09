using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Student
    {
        public Student()
        {
            Payments = new HashSet<Payment>();
        }

        public Guid Idstudent { get; set; }
        public string StudentName { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public Guid Idroom { get; set; }
        public string PaymStatus { get; set; } = null!;
        public decimal Debt { get; set; }

        public virtual Room IdroomNavigation { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
