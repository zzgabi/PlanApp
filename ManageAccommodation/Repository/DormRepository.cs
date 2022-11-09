using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;

namespace ManageAccommodation.Repository
{
    public class DormRepository
    {
        private ApplicationDbContext dbContext;

        public DormRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public DormRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        private DormModel MapDbObjectToModel(Dorm dbdorm)
        {
            DormModel dormModel = new DormModel();

            if(dbdorm != null)
            {
                dormModel.Iddorm = dbdorm.Iddorm;
                dormModel.DormName = dbdorm.DormName;
                dormModel.Adress = dbdorm.Adress;
                dormModel.City = dbdorm.City;
                dormModel.TotalRooms = dbdorm.TotalRooms;
            }
            return dormModel;
        }

        private Dorm MapModelToDbObject(DormModel dormModel)
        {
            Dorm dorm = new Dorm();

            if(dormModel != null)
            {
                dorm.Iddorm = dormModel.Iddorm;
                dorm.DormName = dormModel.DormName;
                dorm.Adress = dormModel.Adress;
                dorm.City =  dormModel.City;
                dorm.TotalRooms = dormModel.TotalRooms;
            }
            return dorm;
        }

        public List<DormModel> GetAllDormsInfo()
        {
            List<DormModel> dormList = new List<DormModel>();

            foreach(Dorm dbDorm in dbContext.Dorms)
            {
                dormList.Add(MapDbObjectToModel(dbDorm));
            }
            return dormList;
        }

        public DormModel GetDormByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Dorms.FirstOrDefault(x => x.Iddorm == ID));
        }

        public void InsertDorm(DormModel dormModel)
        {
            dormModel.Iddorm = Guid.NewGuid();

            dbContext.Dorms.Add(MapModelToDbObject(dormModel));
            dbContext.SaveChanges();
        }

        public void UpdateDorm(DormModel dormModel)
        {
            Dorm existingDoorm = dbContext.Dorms.FirstOrDefault(x => x.Iddorm == dormModel.Iddorm);

            if(existingDoorm != null)
            {
                existingDoorm.Iddorm = dormModel.Iddorm;
                existingDoorm.DormName = dormModel.DormName;
                existingDoorm.Adress = dormModel.Adress;
                existingDoorm.TotalRooms = dormModel.TotalRooms;
                existingDoorm.City = dormModel.City;
                dbContext.SaveChanges();
            }
        }

        public void DeleteDorm(DormModel dormModel)
        {
            Dorm existingDorm = dbContext.Dorms.FirstOrDefault(x => x.Iddorm == dormModel.Iddorm);

            if(existingDorm != null)
            {
                dbContext.Dorms.Remove(existingDorm);
                dbContext.SaveChanges();
            }
        }
    }
}
