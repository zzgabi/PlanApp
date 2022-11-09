using ManageAccommodation.Models;
using ManageAccommodation.Models.DBObjects;

namespace ManageAccommodation.Repository
{
    public class RoomRepository
    {
        private ApplicationDbContext dbContext;

        public RoomRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public RoomRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        private RoomModel MapDbObjectToModel(Room dbroom)
        {
            RoomModel model = new RoomModel();

            if(dbroom != null)
            {
                model.Iddorm = dbroom.Idroom;
                model.OccupiedNo = dbroom.OccupiedNo;
                model.VacanciesNo = dbroom.VacanciesNo;
                model.Capacity = dbroom.Capacity;
                model.Status = dbroom.Status;
                model.Idroom = dbroom.Idroom;
                model.PricePerSt = dbroom.PricePerSt;
            }
            return model;
        }

        private Room MapModelToDbObject(RoomModel model)
        {
            Room room = new Room();

            if(model != null)
            {
                room.Iddorm = model.Iddorm;
                room.OccupiedNo = model.OccupiedNo;
                room.VacanciesNo = model.VacanciesNo;
                room.Capacity = model.Capacity;
                room.Status = model.Status;
                room.Idroom = model.Idroom;
                room.PricePerSt = model.PricePerSt;
            }
            return room;
        }

        public List<RoomModel> GetAllRoomsInfo()
        {
            List<RoomModel> roomList = new List<RoomModel>();

            foreach(Room dbRoom in dbContext.Rooms)
            {
                roomList.Add(MapDbObjectToModel(dbRoom));
            }
            return roomList;
        }

        public RoomModel GetRoomById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Rooms.FirstOrDefault(x => x.Idroom == id));
        }

        public void InsertRoom(RoomModel roomModel)
        {
            roomModel.Idroom = Guid.NewGuid();

            dbContext.Rooms.Add(MapModelToDbObject(roomModel));
            dbContext.SaveChanges();
        }

        public void UpdateRoom(RoomModel roomModel)
        {
            Room existingRoom = dbContext.Rooms.FirstOrDefault(x => x.Idroom == roomModel.Idroom);

            if(existingRoom != null)
            {
                existingRoom.Idroom = roomModel.Idroom;
                existingRoom.OccupiedNo = roomModel.OccupiedNo;
                existingRoom.VacanciesNo = roomModel.VacanciesNo;
                existingRoom.Status = roomModel.Status;
                existingRoom.Capacity = roomModel.Capacity;
                existingRoom.PricePerSt = roomModel.PricePerSt;
                dbContext.SaveChanges();
            }
        }

        public void DeleteRoom(RoomModel roomModel)
        {
            Room existingRoom = dbContext.Rooms.FirstOrDefault(x => x.Idroom == roomModel.Idroom);

            if(existingRoom != null)
            {
                dbContext.Rooms.Remove(existingRoom);
                dbContext.SaveChanges();
            }
        }

    }
}
