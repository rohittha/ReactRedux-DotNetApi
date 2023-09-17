namespace PlantsAPI.Models
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string Name { get; set; }
        public bool IsStatusWatering { get; set; }
        public DateTime LastWatered { get; set; }
}
}
