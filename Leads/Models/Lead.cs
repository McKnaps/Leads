namespace Leads.Models;

public class Lead
{
    public int LeadId { get; set; }
    public PropertyType PropertyType { get; set; } 
    public string Location { get; set; }
    public decimal Price { get; set; }
    public string OwnerContact { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public LeadStatus Status { get; set; } 
    public string RejectionReason { get; set; }
    public int AgentId { get; set; }
    public Agent Agent { get; set; }
}
