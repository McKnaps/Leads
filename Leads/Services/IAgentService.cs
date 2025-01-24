using Leads.DTOs.AgentDTOs;
using Leads.Models;

namespace Leads.Services;

public interface IAgentService
{
    Task<IEnumerable<AgentDTO>> GetAgentsAsync();
    Task<AgentDTO> GetAgentByIdAsync(int id);
    Task<Agent> CreateAgentAsync(AgentCreateDTO agentCreateDto);
    Task<Agent> UpdateAgentAsync(int id, AgentCreateDTO agentCreateDto);
    Task<bool> DeleteAgentAsync(int id);
    Task<AgentDTO> GetAgentByEmailAsync(string email);
}