namespace web.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }
        public int RoomId { get; set; }

        public Room room { get; set; }
        public Amenity amenity { get; set; }
    }
}
