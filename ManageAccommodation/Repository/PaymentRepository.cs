using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;

namespace ManageAccommodation.Repository
{
    public class PaymentRepository
    {
        private ApplicationDbContext dbContext;

        public PaymentRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public PaymentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        private PaymentModel MapDbObjectToModel(Payment dbPaym)
        {
            PaymentModel model = new PaymentModel();

            if(dbPaym != null)
            {
                model.Idpayment = dbPaym.Idpayment;
                model.Iddorm = dbPaym.Iddorm;
                model.Idstudent = dbPaym.Idstudent;
                model.Date = dbPaym.Date;
                model.Idroom = dbPaym.Idroom;
            }
            return model;
        }

        private Payment MapModelToDbObject(PaymentModel model)
        {
            Payment payment = new Payment();

            if(model != null)
            {
                payment.Idpayment = model.Idpayment;
                payment.Iddorm = model.Iddorm;
                payment.Idstudent = model.Idstudent;
                payment.Date = model.Date;
                payment.Idroom = model.Idroom;
            }
            return payment;
        }

        public List<PaymentModel> GetAllPayments()
        {
            List<PaymentModel> list = new List<PaymentModel>();

            foreach(Payment dbPayment in dbContext.Payments)
            {
                list.Add(MapDbObjectToModel(dbPayment));
            }
            return list;
        }

        public PaymentModel GetPaymentById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Payments.FirstOrDefault(x => x.Idpayment == id));
        }

        public void InsertPayment(PaymentModel paymModel)
        {
            paymModel.Idpayment = Guid.NewGuid();

            dbContext.Payments.Add(MapModelToDbObject(paymModel));
            dbContext.SaveChanges();
        }

        public void UpdatePayment(PaymentModel paymModel)
        {
            Payment existingPayment = dbContext.Payments.FirstOrDefault(x => x.Idpayment == paymModel.Idpayment);

            if(existingPayment != null)
            {
                existingPayment.Idpayment = paymModel.Idpayment;
                existingPayment.Iddorm = paymModel.Iddorm;
                existingPayment.Idstudent = paymModel.Idstudent;
                existingPayment.Date = paymModel.Date;
                existingPayment.Idroom = paymModel.Idroom;
                dbContext.SaveChanges();
            }
        }

        public void DeletePayment(PaymentModel paymModel)
        {
            Payment existingPaym = dbContext.Payments.FirstOrDefault(x => x.Idpayment == paymModel.Idpayment);

            if(existingPaym != null)
            {
                dbContext.Payments.Remove(existingPaym);
                dbContext.SaveChanges();
            }
        }
    }
}
