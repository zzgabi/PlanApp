using System.ComponentModel.DataAnnotations;

namespace ManageAccommodation.Models
{
    public class DormModel
    {
        public Guid Iddorm { get; set; }
        public string DormName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Adress { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int TotalRooms { get; set; }


    }
}
