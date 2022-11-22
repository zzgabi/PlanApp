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
        public Guid IdRoom { get; set; }
        public Guid IdPayment { get; set; }
        public string StudentName { get; set; }
        public string DormName { get; set; }
        public decimal Amount { get; set; }
        public decimal Dept { get; set; }
        public string RoomNo { get; set; }
        //public List<SelectListItem> DormDDl { get; set; }
        //public List<SelectListItem> StudentDDl { get; set; }
        //public List<SelectListItem> RoomDDL { get; set; }

        public PaymentViewModel(PaymentModel paymentModel, 
            StudentRepository studentRepository, 
            DormRepository dormRepository, 
            RoomRepository roomRepository)
        {
            this.IdDorm = paymentModel.Iddorm;
            this.IdStudent = paymentModel.Idstudent;
            this.Date = paymentModel.Date;
            this.IdRoom = paymentModel.Idroom;
            var student = studentRepository.GetStudentById(paymentModel.Idstudent);
            this.StudentName = student.StudentName;
            this.Dept = student.Debt;
            this.DormName = dormRepository.GetDormByID(paymentModel.Iddorm).DormName;
            this.RoomNo = roomRepository.GetRoomById(paymentModel.Idroom).Idroom.ToString().Substring(0, 5);
            this.IdPayment = paymentModel.Idpayment;
            //this.DormDDl = studentRepository.GetAllStudents().Select( x => new SelectListItem(x.StudentName, x.Idstudent.ToString())).ToList();


        }
    }
}
