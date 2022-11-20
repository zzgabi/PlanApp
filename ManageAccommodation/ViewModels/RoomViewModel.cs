using ManageAccommodation.Models;
using ManageAccommodation.Repository;

namespace ManageAccommodation.ViewModels
{
    public class RoomViewModel
    {
        public Guid Idroom { get; set; }
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
        public int OccupiedNo { get; set; }
        public int VacanciesNo { get; set; }
        public Guid Iddorm { get; set; }
        public decimal PricePerSt { get; set; }
        public string? DormName { get; set; }

        public RoomViewModel(RoomModel model, DormRepository dormRepository, StudentRepository studentRepository)
        {
            this.Idroom = model.Idroom;
            this.Iddorm = model.Iddorm;
            this.PricePerSt = model.PricePerSt;
            this.Capacity = model.Capacity;
            this.OccupiedNo = model.OccupiedNo;
            this.VacanciesNo = model.VacanciesNo;
            this.Status = model.Status;
            this.DormName = dormRepository.GetDormByID(Iddorm).DormName;
        }
        public RoomViewModel()
        {

        }
    }
}
