using Leads.DTOs.LeadDTOs;
using Leads.Models;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Leads.Services.LeadServices;

public interface ILeadService
{
    Task<Lead> CreateLeadAsync(LeadCreateDTO leadCreateDto);
    Task<IEnumerable<LeadDTO>> GetLeadsByAgentIdAsync(Guid agentId, [FromQuery] SieveModel sieveModel);
    Task<LeadDTOWithAgentDetails> GetLeadById(Guid id);
    Task<Lead> RemoveLeadById(Guid id);
}