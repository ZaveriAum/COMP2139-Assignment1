using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
	public class Photos
	{
		[Key]
		public int photoId {  get; set; }

		[Required]
		public string photoLocation { get; set; }

		public string CarPlateNumber { get; set; }

		public Car? Car { get; set; }

		public string? RoomId { get; set; }

		public Room? Room { get; set; }

		public string? HotelId { get; set; }
		public Hotel? Hotel { get; set; }
	}
}
