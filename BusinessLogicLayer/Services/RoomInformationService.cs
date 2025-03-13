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
    public class RoomInformationService : IService<RoomInformation>
    {
        private readonly IRepository<RoomInformation> _RoomInformationRepository;

        public RoomInformationService()
        {
            _RoomInformationRepository = new RoomInformationRepository();
        }

        public RoomInformation Add(RoomInformation data)
        {
            return _RoomInformationRepository.Add(data);
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

        public RoomInformation? Update(RoomInformation data)
        {
            return _RoomInformationRepository.Update(data);
        }
    }
}
