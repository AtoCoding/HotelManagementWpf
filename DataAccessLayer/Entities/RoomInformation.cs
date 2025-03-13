using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace DataAccessLayer.Entities
{
    public enum RoomStatus
    {
        Active = 1,
        Deleted = 2
    }

    public class RoomInformation
    {
        public int RoomID { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public decimal RoomPricePerDate { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType? RoomType { get; set; }
    }
}