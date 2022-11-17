using ManageAccommodation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageAccommodation.Controllers
{
    public class DormController : Controller
    {

        private Repository.DormRepository _repository;


        public DormController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.DormRepository(dbContext);
        }

        // GET: DormController
        public ActionResult Index()
        {
            var dorms = _repository.GetAllDormsInfo();
            return View("Index", dorms);
        }

        // GET: DormController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetDormByID(id);
            return View("DetailsDorm", model);
        }

        // GET: DormController/Create
        public ActionResult Create()
        {
            return View("CreateDorm");
        }

        // POST: DormController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.DormModel model = new Models.DormModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.InsertDorm(model);
                }
                return View("CreateDorm");
            }
            catch
            {
                return View("CreateDorm");
            }
        }

        // GET: DormController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetDormByID(id);
            return View("EditDorm", model);
        }

        // POST: DormController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new DormModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.UpdateDorm(model);
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

        // GET: DormController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetDormByID(id);
            return View("DeleteDorm", model);
        }

        // POST: DormController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetDormByID(id);
                _repository.DeleteDorm(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteDorm", id);
            }
        }
    }
}
