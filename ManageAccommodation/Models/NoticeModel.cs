using System.ComponentModel.DataAnnotations;

namespace ManageAccommodation.Models
{
    public class NoticeModel
    {
        public Guid Idnotification { get; set; }
        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string EmailStatus { get; set; } = null!;
        public string MobileStatus { get; set; } = null!;
    }
}
