using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageAccommodation.ViewModels
{
    public class StudentViewModel :StudentModel
    {

        public StudentViewModel(StudentModel stModel, RoomRepository roomRepository)
        {
            this.StudentName = stModel.StudentName;
            this.Idstudent = stModel.Idstudent;
            this.Debt = stModel.Debt;
            this.PaymStatus = stModel.PaymStatus;
            this.Idroom = stModel.Idroom;
            this.RoomNo = stModel.Idroom.ToString().Substring(0, 5);
            this.Email = stModel.Email;
            this.Mobile = stModel.Mobile;
            this.PricePerSt = stModel.PricePerSt;

        }
    }
}
