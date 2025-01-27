using Leads.DTOs.AgentDTOs;
using Leads.Models;
using Sieve.Models;

namespace Leads.Services;

public interface IAgentService
{
    Task<IEnumerable<AgentDTO>> GetAgentsAsync(SieveModel sieveModel);
    Task<AgentDTO> GetAgentByIdAsync(Guid id);
    Task<Agent> CreateAgentAsync(AgentDTO agentUpdateDto);
    Task<Agent> UpdateAgentAsync(Guid id, AgentUpdateDTO agentUpdateDto);
    Task<bool> DeleteAgentAsync(Guid id);
    Task<AgentDTO> GetAgentByEmailAsync(string email);
}