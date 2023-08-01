namespace web.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Layout { get; set; }
        public List<AmenityDTO>? Amenities { get; set; }
    }
}
