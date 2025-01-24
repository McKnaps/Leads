namespace Leads.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? AgentId { get; set; }
    public Agent Agent { get; set; } 
}
