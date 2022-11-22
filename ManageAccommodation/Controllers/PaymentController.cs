using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;
using ManageAccommodation.Repository;
using ManageAccommodation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageAccommodation.Controllers
{
    public class PaymentController : Controller
    {
        private Repository.PaymentRepository _paymRepository;
        private Repository.DormRepository _dormRepository;
        private Repository.RoomRepository _roomRepository;
        private Repository.StudentRepository _studentRepository;

        Methods metods = new Methods();
        public PaymentController(ApplicationDbContext dbContext)
        {
            _paymRepository = new Repository.PaymentRepository(dbContext);
            _dormRepository = new Repository.DormRepository(dbContext);
            _roomRepository = new Repository.RoomRepository(dbContext);
            _studentRepository = new Repository.StudentRepository(dbContext);
        }
        // GET: PaymentController
        public ActionResult Index(int pg = 1)
        {
            var payments = _paymRepository.GetAllPayments();
            var paymtViewModel = new List<PaymentViewModel>();
            
            //pager logic
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = payments.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var pagePayments = payments.Skip(recSkip).Take(pager.PageSize);

            foreach(var payment in pagePayments)
            {
                paymtViewModel.Add(new PaymentViewModel(payment, _studentRepository, _dormRepository, _roomRepository));
            }
            var paymTask = from x in paymtViewModel select x;

            return View("Index", paymTask);
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            var studentsDDl = _studentRepository.GetAllStudents().Select(x => new SelectListItem(x.StudentName, x.Idstudent.ToString()));
            var dormsDDl = _dormRepository.GetAllDormsInfo().Select(x => new SelectListItem(x.DormName, x.Iddorm.ToString()));
            var roomsDDl = _roomRepository.GetAllRoomsInfo().Select(x => new SelectListItem(x.Idroom.ToString().Substring(0, 5), x.Idroom.ToString()));
            ViewBag.DormNameDDL = dormsDDl;
            ViewBag.StudentNameDDL = studentsDDl;
            ViewBag.RoomNameDDL = roomsDDl;
            return View("CreatePayment");
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var paymViewModelList = new List<PaymentViewModel>();
                var paymt = new PaymentModel();
                var task1 = TryUpdateModelAsync(paymt);
                var task = TryUpdateModelAsync(paymViewModelList);
                task.Wait();
                task1.Wait();

                _paymRepository.InsertPayment(paymt);
                //foreach (var item in paymViewModelList)
                //{
                //    _paymRepository.InsertPayment(item); 
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePayment");
            }
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
