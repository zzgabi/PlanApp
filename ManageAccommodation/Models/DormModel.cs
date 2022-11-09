namespace ManageAccommodation.Models
{
    public class DormModel
    {
        public Guid Iddorm { get; set; }
        public string DormName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int TotalRooms { get; set; }
    }
}
