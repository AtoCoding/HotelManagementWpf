using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services
{
    public class RoomTypeService : IService<RoomType>
    {
        private static RoomTypeService _Instance = null!;
        private readonly IRepository<RoomType> _RoomTypeRepository;

        private RoomTypeService()
        {
            _RoomTypeRepository = RoomTypeRepository.GetInstance();
        }

        public static RoomTypeService GetInstance() => _Instance ??= new RoomTypeService();

        public RoomType Add(RoomType data)
        {
            return _RoomTypeRepository.Add(data);
        }

        public int Count()
        {
            return _RoomTypeRepository.Count();
        }

        public bool Delete(int id)
        {
            return _RoomTypeRepository.Delete(id);
        }

        public RoomType? Get(int id)
        {
            return _RoomTypeRepository.Get(id);
        }

        public List<RoomType> GetAll()
        {
            return _RoomTypeRepository.GetAll();
        }

        public List<RoomType> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<RoomType> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public RoomType? Update(RoomType data)
        {
            return _RoomTypeRepository.Update(data);
        }
    }
}
