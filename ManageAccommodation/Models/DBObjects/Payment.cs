using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Payment
    {
        public Guid Idpayment { get; set; }
        public Guid Iddorm { get; set; }
        public Guid Idstudent { get; set; }
        public DateTime Date { get; set; }
        public Guid Idroom { get; set; }

        public virtual Dorm IddormNavigation { get; set; } = null!;
        public virtual Room IdroomNavigation { get; set; } = null!;
        public virtual Student IdstudentNavigation { get; set; } = null!;
    }
}
