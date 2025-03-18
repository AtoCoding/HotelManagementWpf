using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private static RoomTypeRepository _Instance = null!;
        private readonly List<RoomType> _RoomTypes;

        private RoomTypeRepository()
        {
            IConfiguration config = Utils.GetConfiguration();
            _RoomTypes = config.GetSection("RoomType").Get<List<RoomType>>() ?? [];
        }

        public static RoomTypeRepository GetInstance() => _Instance ??= new RoomTypeRepository();

        public RoomType Add(RoomType data)
        {
            _RoomTypes.Add(data);
            return data;
        }

        public int Count()
        {
            return _RoomTypes.Count;
        }

        public bool Delete(int id)
        {
            RoomType? roomType = _RoomTypes.FirstOrDefault(x => x.RoomTypeID == id);
            return _RoomTypes.Remove(roomType!);
        }

        public RoomType? Get(int id)
        {
            return _RoomTypes.FirstOrDefault(x => x.RoomTypeID == id);
        }

        public List<RoomType> GetAll()
        {
            return _RoomTypes;
        }

        public List<RoomType> Search(string description, string typeName, int capacity)
        {
            return [];
        }

        public RoomType? Update(RoomType data)
        {
            RoomType? roomType = _RoomTypes.FirstOrDefault(x => x.RoomTypeID == data.RoomTypeID);
            if (roomType != null)
            {
                roomType.RoomTypeName = data.RoomTypeName;
                roomType.TypeDescription = data.TypeDescription;
            }
            return roomType;
        }
    }
}
