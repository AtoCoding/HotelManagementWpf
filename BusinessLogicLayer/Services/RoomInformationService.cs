using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services
{
    public class RoomInformationService : IService<RoomInformation>
    {
        private static RoomInformationService _Instance = null!;
        private readonly IRepository<RoomInformation> _RoomInformationRepository;
        private readonly IRepository<RoomType> _RoomTypeRepository;

        private RoomInformationService()
        {
            _RoomInformationRepository = RoomInformationRepository.GetInstance();
            _RoomTypeRepository = RoomTypeRepository.GetInstance();
        }

        public static RoomInformationService GetInstance() => _Instance ??= new RoomInformationService();

        public RoomInformation Add(RoomInformation data)
        {
            if (data != null)
            {
                List<RoomType> roomTypes = _RoomTypeRepository.GetAll();
                data.RoomType = roomTypes.FirstOrDefault(x => x.RoomTypeID == data.RoomTypeID);
                return _RoomInformationRepository.Add(data);
            }

            return null!;
        }

        public int Count()
        {
            return _RoomInformationRepository.Count();
        }

        public bool Delete(int id)
        {
            return _RoomInformationRepository.Delete(id);
        }

        public RoomInformation? Get(int id)
        {
            return _RoomInformationRepository.Get(id);
        }

        public List<RoomInformation> GetAll()
        {
            return _RoomInformationRepository.GetAll();
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            return _RoomInformationRepository.Search(description, typeName, capacity);
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public List<RoomInformation> Search(string? description, string? typeName, int? capacity)
        {
            throw new NotImplementedException();
        }

        public RoomInformation? Update(RoomInformation data)
        {
            if (data != null)
            {
                List<RoomType> roomTypes = _RoomTypeRepository.GetAll();
                data.RoomType = roomTypes.FirstOrDefault(x => x.RoomTypeID == data.RoomTypeID);
                return _RoomInformationRepository.Update(data);
            }

            return null!;
        }
    }
}
