using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;
using ManageAccommodation.ViewModels;

namespace ManageAccommodation.Repository
{
    public class StudentRepository
    {
        private ApplicationDbContext dbContext;

        public StudentRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        private StudentModel MapDbObjectToModel(Student dbStudent)
        {
            StudentModel model = new StudentModel();

            if(dbStudent != null){
                model.Idstudent = dbStudent.Idstudent;
                model.StudentName = dbStudent.StudentName;
                model.Mobile = dbStudent.Mobile;
                model.Idroom = dbStudent.Idroom;
                model.PaymStatus = dbStudent.PaymStatus;
                model.Debt = dbStudent.Debt;
                model.Email = dbStudent.Email;
            }
            return model;
        }

        private Student MapModelToDbObject(StudentModel model)
        {
            Student student = new Student();

            if(model != null)
            {
                student.StudentName = model.StudentName;
                student.Mobile = model.Mobile;  
                student.Idroom = model.Idroom;
                student.PaymStatus = model.PaymStatus;
                student.Debt = model.Debt;
                student.Idstudent = model.Idstudent;
                student.Email = model.Email;
            }
            return student;
        }

        public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> studentList = new List<StudentModel>();

            foreach(Student dbStudent in dbContext.Students)
            {
                studentList.Add(MapDbObjectToModel(dbStudent));
            }
            return studentList;
        }

        public StudentModel GetStudentById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Students.FirstOrDefault(x => x.Idstudent == id));
        }

        public Guid GetIdRoomByStudentId(Guid id)
        {
            return MapDbObjectToModel(dbContext.Students.FirstOrDefault(x => x.Idstudent == id)).Idroom;
        }

        public List<StudentModel> GetStudentsByIdRoom(Guid id)
        {
            List<StudentModel> studentList = new List<StudentModel>();

            foreach (Student dbStudent in dbContext.Students)
            {
                if (dbStudent.Idroom == id)
                    studentList.Add(MapDbObjectToModel(dbStudent));
            }
            return studentList;
        }

        public void InsertStudent(StudentModel studentModel)
        {
            studentModel.Idstudent = Guid.NewGuid();

            studentModel.PaymStatus = studentModel.Debt == 0 ? "Paid" : "Unpaid";
            
            dbContext.Students.Add(MapModelToDbObject(studentModel));
            dbContext.SaveChanges();
        }


        public void UpdateStudent(StudentModel studentModel)
        {
            Student existingStudent = dbContext.Students.FirstOrDefault(x => x.Idstudent == studentModel.Idstudent);

            if(existingStudent != null)
            {
                existingStudent.Idstudent = studentModel.Idstudent;
                existingStudent.StudentName = studentModel.StudentName;
                existingStudent.Mobile = studentModel.Mobile;
                existingStudent.Email = studentModel.Email;
                existingStudent.Idroom = studentModel.Idroom;
                existingStudent.Debt = studentModel.Debt;

                existingStudent.PaymStatus = studentModel.Debt == 0 ? "Paid" : "Unpaid";                

                dbContext.SaveChanges();
            }
        }

        public void UpdateDebt(Guid id, decimal amount)
        {
            Student existingStudent = dbContext.Students.FirstOrDefault(x => x.Idstudent == id);
            if(existingStudent != null)
            {
                existingStudent.Debt -= amount;
                existingStudent.PaymStatus = existingStudent.Debt == 0 ? "Paid" : "Unpaid";

                dbContext.SaveChanges();
            }
        }

        public void UpdatePayments(StudentModel studentModel)
        {
            Student existingStudent = dbContext.Students.FirstOrDefault(x => x.Idstudent == studentModel.Idstudent);
            
            var model = dbContext.Rooms.FirstOrDefault(x => x.Idroom == studentModel.Idroom);

            if(existingStudent != null)
            {
                existingStudent.Debt = existingStudent.Debt + model.PricePerSt;
                if (existingStudent.Debt >= 0)
                    existingStudent.PaymStatus = "Unpaid";

                dbContext.SaveChanges();
            }
        }
        public void DeleteStudent(StudentModel studentModel)
        {
            Student existingStudent = dbContext.Students.FirstOrDefault(x => x.Idstudent == studentModel.Idstudent);

            if(existingStudent != null)
            {
                DeleteCascadePayments(existingStudent.Idstudent);

                dbContext.Students.Remove(existingStudent);
                dbContext.SaveChanges();
            }
        }

        public void DeleteCascadePayments(Guid id)
        {
            var payms = dbContext.Payments.Where(x => x.Idstudent == id);
            foreach(var item in payms)
            {
                dbContext.Payments.Remove(item);
            }
        }
    }
}
