using System.ComponentModel.DataAnnotations;

namespace ManageAccommodation.Models
{
    public class PaymentModel
    {
        public Guid Idpayment { get; set; }
        public Guid Iddorm { get; set; }
        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public Guid Idstudent { get; set; }
        public DateTime Date { get; set; }
        public Guid Idroom { get; set; }
    }
}
