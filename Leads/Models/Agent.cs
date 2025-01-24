using Sieve.Attributes;

namespace Leads.Models;

public class Agent
{
    [Sieve(CanFilter = true, CanSort = true)]
    public int AgentId { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string FirstName { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string LastName { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string Email { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string OperatingArea { get; set; }
    public ICollection<Lead> Leads { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}