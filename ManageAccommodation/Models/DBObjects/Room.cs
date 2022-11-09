using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Room
    {
        public Room()
        {
            Payments = new HashSet<Payment>();
            Students = new HashSet<Student>();
        }

        public Guid Idroom { get; set; }
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
        public int OccupiedNo { get; set; }
        public int VacanciesNo { get; set; }
        public Guid Iddorm { get; set; }
        public decimal PricePerSt { get; set; }

        public virtual Dorm IddormNavigation { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
