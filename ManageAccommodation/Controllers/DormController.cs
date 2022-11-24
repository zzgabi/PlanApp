using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public IActionResult Index(int pg = 1)
        {
            var dorms = _repository.GetAllDormsInfo();
            //pager logic
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = dorms.Count();

            var pager = new Pager("Dorm", recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var pageDorms = dorms.Skip(recSkip).Take(pager.PageSize);
            this.ViewBag.Pager = pager;

            return View("Index", dorms);
        }

        // GET: DormController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetDormByID(id);
            return View("DetailsDorm", model);
        }

        [Authorize(Roles = "KeyUser, Admin")]
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
        [Authorize(Roles = "KeyUser, Admin")]
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
        [Authorize(Roles = "Admin")]
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
