namespace Leads.DTOs.AgentDTOs;

public class AgentDTO
{
    public Guid AgentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string OperatingArea { get; set; }
}