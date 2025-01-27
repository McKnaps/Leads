namespace Leads.DTOs.LeadDTOs;

public class LeadCreateDTO
{
    public PropertyType PropertyType { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public string OwnerContact { get; set; }
    public Guid AgentId { get; set; }
}