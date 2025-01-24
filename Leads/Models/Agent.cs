namespace Leads.Models;

public class Agent
{
    public int AgentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string OperatingArea { get; set; }
    public ICollection<Lead> Leads { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}