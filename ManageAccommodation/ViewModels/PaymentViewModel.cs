using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlTypes;

namespace ManageAccommodation.ViewModels
{
    public class PaymentViewModel
    {
        public Guid IdDorm { get; set; }
        public Guid IdStudent { get; set; }
        public DateTime Date { get; set; }
        public Guid IdPayment { get; set; }
        public string StudentName { get; set; }
        public string DormName { get; set; }
        public decimal Amount { get; set; }
        public decimal Dept { get; set; }
        public string RoomNo { get; set; }

        public PaymentViewModel(PaymentModel paymentModel, 
            StudentRepository studentRepository, 
            DormRepository dormRepository, 
            RoomRepository roomRepository)
        {
            Random rn = new Random();
            this.IdPayment = paymentModel.Idpayment;
            this.IdStudent = paymentModel.Idstudent;
            this.IdDorm = paymentModel.Iddorm;
            this.Date = paymentModel.Date;
            this.RoomNo =paymentModel.Idroom.ToString().Substring(0, 3);
            var student = studentRepository.GetStudentById(paymentModel.Idstudent);
            this.DormName = dormRepository.GetDormByID(paymentModel.Iddorm).DormName;
            this.StudentName = student.StudentName;
            this.Dept = student.Debt;
            this.Amount = rn.Next(250);

        }
    }
}
