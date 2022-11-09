namespace ManageAccommodation.Models
{
    public class StudentModel
    {
        public Guid Idstudent { get; set; }
        public string StudentName { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public Guid Idroom { get; set; }
        public string PaymStatus { get; set; } = null!;
        public decimal Debt { get; set; }
    }
}
