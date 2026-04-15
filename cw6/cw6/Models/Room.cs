namespace cw6.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public String BuildingCode { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
    
}