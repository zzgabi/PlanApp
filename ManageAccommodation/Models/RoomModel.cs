using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ManageAccommodation.Models
{
    public class RoomModel : DormModel
    {
    
        public Guid Idroom { get; set; }
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
        public int OccupiedNo { get; set; }
        public int VacanciesNo { get; set; }
        public decimal PricePerSt { get; set; }
    }
}
