using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services
{
    public class RoomTypeService : IService<RoomType>
    {
        private readonly IRepository<RoomType> _RoomTypeRepository;

        public RoomTypeService()
        {
            _RoomTypeRepository = new RoomTypeRepository();
        }

        public RoomType Add(RoomType data)
        {
            return _RoomTypeRepository.Add(data);
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

        public RoomType? Update(RoomType data)
        {
            return _RoomTypeRepository.Update(data);
        }
    }
}
