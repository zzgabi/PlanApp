namespace ManageAccommodation.Models
{
    public class StudentModel : RoomModel
    {
        public Guid Idstudent { get; set; }
        public string StudentName { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string PaymStatus { get; set; } = null!;
        public decimal Debt { get; set; }

        public string? RoomNo { get; set; }
    }
}
