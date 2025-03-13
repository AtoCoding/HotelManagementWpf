using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class RoomInformationRepository : IRepository<RoomInformation>
    {
        private List<RoomInformation> _RoomInformations;
        private RoomTypeRepository _RoomTypeRepository;

        public RoomInformationRepository()
        {
            _RoomTypeRepository = new RoomTypeRepository();
            IConfiguration config = Utils.GetConfiguration();
            _RoomInformations = config.GetSection("RoomInformation").Get<List<RoomInformation>>() ?? [];
            LoadRoomType(_RoomInformations);
        }

        public RoomInformation Add(RoomInformation data)
        {
            _RoomInformations.Add(data);
            return data;
        }

        public int Count()
        {
            return _RoomInformations.Count;
        }

        public bool Delete(int id)
        {
            RoomInformation? roomInformation = _RoomInformations.FirstOrDefault(x => x.RoomID == id);
            return _RoomInformations.Remove(roomInformation!);
        }

        public RoomInformation? Get(int id)
        {
            return _RoomInformations.FirstOrDefault(x => x.RoomID == id);
        }

        public List<RoomInformation> GetAll()
        {
            return _RoomInformations;
        }

        public RoomInformation? Update(RoomInformation data)
        {
            RoomInformation? dataSearched = _RoomInformations.FirstOrDefault(x => x.RoomID == data.RoomID);

            if (dataSearched != null)
            {
                dataSearched.RoomNumber = data.RoomNumber;
                dataSearched.RoomDescription = data.RoomDescription;
                dataSearched.RoomMaxCapacity = data.RoomMaxCapacity;
                dataSearched.RoomStatus = data.RoomStatus;
                dataSearched.RoomPricePerDate = data.RoomPricePerDate;
                dataSearched.RoomTypeID = data.RoomTypeID;
                dataSearched.RoomType = data.RoomType;
            }

            return dataSearched;
        }

        private List<RoomInformation> LoadRoomType(List<RoomInformation> roomInformations)
        {
            List<RoomType> roomTypes = _RoomTypeRepository.GetAll();

            foreach (RoomInformation roomInformation in roomInformations)
            {
                roomInformation.RoomType = roomTypes.FirstOrDefault(x => x.RoomTypeID == roomInformation.RoomTypeID);
            }

            return roomInformations;
        }
    }
}
