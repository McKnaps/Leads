using Leads.DTOs.LeadDTOs;
using Leads.Models;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Leads.Services.LeadServices;

public interface ILeadService
{
    Task<Lead> CreateLeadAsync(LeadCreateDTO leadCreateDto);
    Task<IEnumerable<LeadDTO>> GetLeadsByAgentIdAsync(int agentId, [FromQuery] SieveModel sieveModel);
    Task<LeadDTOWithAgentDetails> GetLeadById(int id);
    Task<Lead> RemoveLeadById(int id);
}