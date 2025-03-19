using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class RoomInformationRepository : IRepository<RoomInformation>
    {
        private static RoomInformationRepository _Instance = null!;
        private readonly List<RoomInformation> _RoomInformations;
        private readonly RoomTypeRepository _RoomTypeRepository;

        private RoomInformationRepository()
        {
            IConfiguration config = Utils.GetConfiguration();
            _RoomInformations = config.GetSection("RoomInformation").Get<List<RoomInformation>>() ?? [];
            _RoomTypeRepository = RoomTypeRepository.GetInstance();
            LoadRoomType(_RoomInformations);
        }

        public static RoomInformationRepository GetInstance() => _Instance ??= new RoomInformationRepository();

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

            if (roomInformation != null)
            {
                roomInformation.RoomStatus = RoomStatus.Deleted;
                return true;
            }

            return false;
        }

        public RoomInformation? Get(int id)
        {
            return _RoomInformations.FirstOrDefault(x => x.RoomID == id);
        }

        public List<RoomInformation> GetAll()
        {
            return _RoomInformations;
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            List<RoomInformation> result = _RoomInformations.ToList();

            if (!string.IsNullOrEmpty(description))
            {
                result.RemoveAll(x => !x.RoomDescription!.ToLower().Contains(description.ToLower()));
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                result.RemoveAll(x => !x.RoomType!.RoomTypeName!.ToLower().Contains(typeName.ToLower()));
            }
            if (capacity > 0)
            {
                result.RemoveAll(x => x.RoomMaxCapacity != capacity);
            }

            return result;
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
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
