namespace Leads.DTOs.LeadDTOs;

public class LeadDTO
{
    public Guid LeadId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public PropertyType PropertyType { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public string OwnerContact { get; set; }
    public string? RejectionReason { get; set; }
    public Guid AgentId { get; set; }
    public LeadStatus Status { get; set; }
}