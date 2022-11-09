using System;
using System.Collections.Generic;

namespace ManageAccommodation.Models.DBObjects
{
    public partial class Administrator
    {
        public Guid Idadministrator { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid Iddorm { get; set; }

        public virtual Dorm IddormNavigation { get; set; } = null!;
    }
}
