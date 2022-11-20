using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageAccommodation.Controllers
{
    public class StudentController : Controller
    {

        private Repository.StudentRepository _repository;
        private Repository.RoomRepository _roomRepository;

        public StudentController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.StudentRepository(dbContext);
            _roomRepository = new RoomRepository(dbContext);
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var students = _repository.GetAllStudents();
            foreach(var student in students)
            {
                student.RoomNo = _roomRepository.GetRoomById(student.Idroom).Idroom.ToString().Substring(0, 5);
            }
            return View("Index", students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetStudentById(id);
            return View("StudentDetails", model);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            List<SelectListItem> Status = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Paid", Value="Paid"},
                new SelectListItem() { Text="Unpaid", Value="Unpaid"},
            };
            var rooms = _roomRepository.GetAllFreeRooms().Select(x => new SelectListItem(x.Idroom.ToString().Substring(0, 5), x.Idroom.ToString()));
            ViewBag.RoomNo = rooms;
            ViewBag.Status = Status;
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
            List<SelectListItem> Status = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Paid", Value="Paid"},
                new SelectListItem() { Text="Unpaid", Value="Unpaid"},
            };
            var rooms = _roomRepository.GetAllRoomsInfo().Select(x => new SelectListItem(x.Idroom.ToString().Substring(0, 5), x.Idroom.ToString()));
            ViewBag.RoomNo = rooms;
            ViewBag.Status = Status;

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
