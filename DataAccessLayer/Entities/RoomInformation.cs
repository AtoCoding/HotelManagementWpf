using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Room's Number is required.")]
        [StringLength(4, ErrorMessage = "Room's Number cannot exceed 4 characters.")]
        public string? RoomNumber { get; set; }

        [Required(ErrorMessage = "Room's Description is required.")]
        [StringLength(200, ErrorMessage = "Room's Description cannot exceed 200 characters.")]
        public string? RoomDescription { get; set; }

        [Required(ErrorMessage = "Max capacity is not a number.")]
        [Range(1, 30, ErrorMessage = "Max capacity must be between 1 and 30.")]
        public int? RoomMaxCapacity { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public RoomStatus? RoomStatus { get; set; }

        [Required(ErrorMessage = "Price($) is not a number.")]
        [Range(0, 10000, ErrorMessage = "Price($) must be between 0 and 10,000.")]
        public decimal? RoomPricePerDate { get; set; }

        [Required(ErrorMessage = "Room's type is required.")]
        public int? RoomTypeID { get; set; }

        public RoomType? RoomType { get; set; }
    }
}