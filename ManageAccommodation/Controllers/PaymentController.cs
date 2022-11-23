using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;
using ManageAccommodation.Repository;
using ManageAccommodation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageAccommodation.Controllers
{
    [Authorize(Roles = "KeyUser, Admin")]
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

            this.ViewBag.Pager = pager;
            return View("Index", paymTask);
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(Guid id)
        {
            var paymModel = _paymRepository.GetPaymentById(id);

            var paymViewModel = new PaymentViewModel(paymModel, _studentRepository, _dormRepository, _roomRepository);
            
            return View("PaymentDetails", paymViewModel);
        }
        [Authorize(Roles ="KeyUser, Admin")]
        // GET: PaymentController/Create
        public ActionResult Create()
        {
            var studentsDDl = _studentRepository.GetAllStudents().Select(x => new SelectListItem(x.StudentName, x.Idstudent.ToString()));
            ViewBag.StudentNameDDL = studentsDDl;
            return View("CreatePayment");
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var paymt = new PaymentModel();
                var task1 = TryUpdateModelAsync(paymt);
                paymt.Idroom = _studentRepository.GetStudentById(paymt.Idstudent).Idroom;
                paymt.Iddorm = _roomRepository.GetRoomById(paymt.Idroom).Iddorm;

                task1.Wait();

                _paymRepository.InsertPayment(paymt);
                _studentRepository.UpdateDebt(paymt.Idstudent, paymt.Amount);


                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePayment");
            }
        }

        // GET: PaymentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PaymentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //GET: PaymentController/Delete/5
        [Authorize(Roles = "KeyUser, Admin")]
        public ActionResult Delete(Guid id)
        {
            var model = _paymRepository.GetPaymentById(id);
            var paymViewModel = new PaymentViewModel(model, _studentRepository, _dormRepository, _roomRepository);

            return View("DeletePayment", paymViewModel);
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _paymRepository.GetPaymentById(id);
                _paymRepository.DeletePayment(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeletePayment", id);
            }
        }
    }
}
