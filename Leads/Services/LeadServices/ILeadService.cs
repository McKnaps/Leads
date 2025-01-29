using System.Security.Claims;
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
    Task<IEnumerable<LeadDTO>> GetLeadsForCurrentAgentAsync(ClaimsPrincipal user, SieveModel sieveModel);

    Task<LeadDTO> UpdateLeadStatusAsync(Guid leadId, LeadStatus newStatus, ClaimsPrincipal user, string rejectionReason = null);
}