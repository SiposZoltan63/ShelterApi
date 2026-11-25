namespace ShelterApi.Models.DTOs
{
    public class NewAnimalsDTO
    {
        public string? Name { get; set; }
        public int Species { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public Guid ShleterID { get; set; }
    }
}
