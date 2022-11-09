using ManageAccommodation.Models;
using ManageAccommodation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageAccommodation.Controllers
{
    public class RoomController : Controller
    {
        private Repository.RoomRepository _repository;

        public RoomController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.RoomRepository(dbContext);
        }


        // GET: RoomController
        public ActionResult Index()
        {
            var rooms = _repository.GetAllRoomsInfo();
            return View("Index", rooms);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetRoomById(id);
            return View("RoomDetails", model);
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View("CreateRoom");
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.RoomModel model = new Models.RoomModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.InsertRoom(model);
                }
                return View("CreateRoom");
            }
            catch
            {
                return View("CreateRoom");
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetRoomById(id);
            return View("EditRoom", model);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new RoomModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.UpdateRoom(model);
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

        // GET: RoomController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetRoomById(id);
            return View("DeleteRoom", model);
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetRoomById(id);
                _repository.DeleteRoom(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteRoom", id);
            }
        }
    }
}
