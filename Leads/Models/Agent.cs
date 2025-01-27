using Sieve.Attributes;

namespace Leads.Models;

public class Agent
{
    [Sieve(CanFilter = true, CanSort = true)]
    public Guid AgentId { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string FirstName { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string LastName { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string Email { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string OperatingArea { get; set; }
    public ICollection<Lead> Leads { get; set; }
}