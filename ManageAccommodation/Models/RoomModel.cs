namespace ManageAccommodation.Models
{
    public class RoomModel
    {
        public Guid Idroom { get; set; }
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
        public int OccupiedNo { get; set; }
        public int VacanciesNo { get; set; }
        public Guid Iddorm { get; set; }
        public decimal PricePerSt { get; set; }
    }
}
