using Sieve.Attributes;

namespace Leads.Models;

public class Lead
{
    public Guid LeadId { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public PropertyType PropertyType { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string Location { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public decimal Price { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string OwnerContact { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public LeadStatus Status { get; set; } 
    public string RejectionReason { get; set; }
    public Guid AgentId { get; set; }
    public Agent Agent { get; set; }
}
