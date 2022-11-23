using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using ManageAccommodation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Configuration;

namespace ManageAccommodation.Controllers
{
    public class StudentController : Controller
    {


        private Repository.StudentRepository _repository;
        private Repository.RoomRepository _roomRepository;
        Methods metods = new Methods();


        public StudentController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.StudentRepository(dbContext);
            _roomRepository = new RoomRepository(dbContext);

        }

        // GET: StudentController
        public async Task<IActionResult> Index(string sortOrder, int pg = 1)
        {
            var students = _repository.GetAllStudents();
            ////to do: update payments once per month
            //var date = DateTime.Now.Day;
            //if (date - 1 == 9)    //NOT OK / to add status or mark
            //{
            //    foreach (var student in students)
            //    {
            //        _repository.UpdatePayments(student);
            //    }
            //}
            var studentViewModelList = new List<StudentViewModel>();


            //pager logic
            const int pageSize = 10;

            if (pg < 1)
                pg = 1;

            int recsCount = students.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var pageStudents = students.Skip(recSkip).Take(pager.PageSize);

            foreach (var student in pageStudents)
            {
                studentViewModelList.Add(new StudentViewModel(student, _roomRepository));
                //student.RoomNo = _roomRepository.GetRoomById(student.Idroom).Idroom.ToString().Substring(0, 5);
            }

            //sort logic
            var studentTask = from x in studentViewModelList select x;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PaymStatusParm"] = String.IsNullOrEmpty(sortOrder) ? "unpaid" : "paid";
            ViewData["DebtParm"] = String.IsNullOrEmpty(sortOrder) ? "debt_desc" : "debt";



            switch (sortOrder)
            {
                case "name_desc":
                    studentTask = studentTask.OrderByDescending(x => x.StudentName).ToList();
                    break;
                case "paid":
                    studentTask = studentTask.OrderBy(s => s.PaymStatus);
                    //studentTask = studentTask.Where(s => s.PaymStatus == "Paid");
                    break;
                case "unpaid":
                    studentTask = studentTask.OrderByDescending(x => x.PaymStatus);
                    //studentTask = studentTask.Where(s => s.PaymStatus == "Unpaid");
                    break;
                case "debt_desc":
                    studentTask = studentTask.OrderByDescending(x => x.Debt);
                    break;
                case "debt":
                    studentTask = studentTask.OrderBy(x => x.Debt);
                    break;
                default:
                    studentTask = studentTask.OrderBy(s => s.StudentName);
                    break;

            }




            this.ViewBag.Pager = pager;
            return View("Index", studentTask);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetStudentById(id);
            model.RoomNo = _repository.GetIdRoomByStudentId(id).ToString().Substring(0, 5);
            return View("StudentDetails", model);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            var rooms = _roomRepository.GetAllFreeRooms().Select(x => new SelectListItem(x.Idroom.ToString().Substring(0, 5), x.Idroom.ToString()));
            ViewBag.RoomNo = rooms;
            ViewBag.Status = metods.Status;
            return View("CreateStudent");
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new StudentModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();

                _repository.InsertStudent(model);
                var room = _roomRepository.GetRoomById(model.Idroom);
                _roomRepository.UpdateRoomOnAddStudent(room);
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateStudent");
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            
            var rooms = _roomRepository.GetAllRoomsInfo().Select(x => new SelectListItem(x.Idroom.ToString().Substring(0, 5), x.Idroom.ToString()));
            ViewBag.RoomNo = rooms;
            ViewBag.Status = metods.Status;

            var model = _repository.GetStudentById(id);
            return View("EditStudent", model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new StudentModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                _repository.UpdateStudent(model);
                return RedirectToAction("Index");
                
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetStudentById(id);
            model.RoomNo = _roomRepository.GetRoomById(model.Idroom).Idroom.ToString().Substring(0, 5);

            return View("DeleteStudent", model);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                //to do: get room by student id  / update status
                Guid roomId = _repository.GetIdRoomByStudentId(id);
                var room = _roomRepository.GetRoomById(roomId);
                //update status room
                _roomRepository.UpdateRoomOnStudentDelete(room);

                var model = _repository.GetStudentById(id);
                _repository.DeleteStudent(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteStudent", id);
            }
        }
    }
}
