﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private List<RoomType> _RoomTypes;

        public RoomTypeRepository()
        {
            IConfiguration config = Utils.GetConfiguration();
            _RoomTypes = config.GetSection("RoomType").Get<List<RoomType>>() ?? [];
        }

        public RoomType Add(RoomType data)
        {
            _RoomTypes.Add(data);
            return data;
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
