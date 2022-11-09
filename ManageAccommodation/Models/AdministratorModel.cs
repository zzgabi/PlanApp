namespace ManageAccommodation.Models
{
    public class AdministratorModel
    {
        public Guid Idadministrator { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid Iddorm { get; set; }
    }
}
