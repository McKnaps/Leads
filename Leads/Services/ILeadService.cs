using Leads.DTOs.LeadDTOs;
using Leads.Models;
using Microsoft.AspNetCore.Mvc;

namespace Leads.Services;

public interface ILeadService
{
    Task<Lead> CreateLeadAsync(LeadCreateDTO leadCreateDto);
    Task<IEnumerable<LeadDTO>> GetLeadsByAgentIdAsync(int agentId);
    Task<LeadDTOWithAgentDetails> GetLeadById(int id);
    Task<Lead> RemoveLeadById(int id);
}