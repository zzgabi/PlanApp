using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Notice
    {
        public Guid Idnotification { get; set; }
        public DateTime Date { get; set; }
        public string EmailStatus { get; set; } = null!;
        public string MobileStatus { get; set; } = null!;
    }
}
