using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageAccommodation.Models
{
    public class RoomModel : DormModel
    {
    
        public Guid Idroom { get; set; }
        [Required]
        public string Status { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Capacity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int OccupiedNo { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int VacanciesNo { get; set; }
        [Column(TypeName = "money")]
        public decimal PricePerSt { get; set; }

        public string? RoomNo { get; set; }


    }
}
