using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageAccommodation.Controllers
{
    public class StudentController : Controller
    {

        private Repository.StudentRepository _repository;

        public StudentController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.StudentRepository(dbContext);
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var students = _repository.GetAllStudents();
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
            return View("CreateStudent");
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.StudentModel model = new Models.StudentModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.InsertStudent(model);
                }
                return View("CreateStudent");
            }
            catch
            {
                return View("CreateStudent");
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(Guid id)
        {
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

                if (task.Result)
                {
                    _repository.UpdateStudent(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
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
            return View("DeleteStudent", model);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
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
