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
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public string OperatingArea { get; set; }
    public ICollection<Lead> Leads { get; set; }
}