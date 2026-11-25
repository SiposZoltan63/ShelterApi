namespace ShelterApi.Models.DTOs
{
    public class UpdateAnimalsDTO
    {
        public string? Name { get; set; }
        public int Species { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
    }
}
