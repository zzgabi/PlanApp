using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Dorm
    {
        public Dorm()
        {
            Administrators = new HashSet<Administrator>();
            Payments = new HashSet<Payment>();
            Rooms = new HashSet<Room>();
        }

        public Guid Iddorm { get; set; }
        public string DormName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int TotalRooms { get; set; }

        public virtual ICollection<Administrator> Administrators { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
