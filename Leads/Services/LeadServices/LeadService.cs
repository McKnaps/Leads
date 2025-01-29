
using System.Security.Claims;
using AutoMapper;
using Leads.Data;
using Leads.DTOs.AgentDTOs;
using Leads.DTOs.LeadDTOs;
using Leads.Models;
using Leads.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Leads.Services.LeadServices;

public class LeadService : ILeadService
{
    private readonly IAgentService _agentService;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly SieveProcessor _sieveProcessor;

    public LeadService(IAgentService agentService, ApplicationDbContext context, IMapper mapper, SieveProcessor sieveProcessor)
    {
        _agentService = agentService;
        _context = context;
        _mapper = mapper;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<Lead> CreateLeadAsync(LeadCreateDTO leadCreateDto)
    {
        var lead = _mapper.Map<Lead>(leadCreateDto);
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
        return lead;
    }

    public async Task<IEnumerable<LeadDTO>> GetLeadsByAgentIdAsync(Guid agentId, [FromQuery] SieveModel sieveModel)
    {
        var query = _context.Leads.Where(lead => lead.AgentId == agentId).AsQueryable();
        
        query = _sieveProcessor.Apply(sieveModel, query);
        var leads = await query.ToListAsync();

        return _mapper.Map<IEnumerable<LeadDTO>>(leads);
    }

    public async Task<LeadDTOWithAgentDetails> GetLeadById(Guid id)
    {
        var lead = await _context.Leads.FirstOrDefaultAsync(x => x.LeadId == id);
        if (lead == null)
        {
            throw new KeyNotFoundException($"Lead with ID {id} was not found.");
        }
        
        var agent = await _agentService.GetAgentByIdAsync(lead.AgentId);
        if (agent == null)
        {
            throw new KeyNotFoundException($"Agent with ID {lead.AgentId} was not found.");
        }
        
        var leadDto = _mapper.Map<LeadDTOWithAgentDetails>(lead);
        if (leadDto == null)
        {
            throw new InvalidOperationException("Mapping Lead to LeadDto failed.");
        }
        
        leadDto.AgentDto = _mapper.Map<AgentDTO>(agent);
        if (leadDto.AgentDto == null)
        {
            throw new InvalidOperationException("Mapping Agent to AgentDto failed.");
        }

        return leadDto;
    }


    public async Task<Lead> RemoveLeadById(Guid id)
    {
        var removeLead = await _context.Leads.FindAsync(id);
        _context.Leads.Remove(removeLead);
        await _context.SaveChangesAsync();
        return removeLead;
    }
    public async Task<IEnumerable<LeadDTO>> GetLeadsForCurrentAgentAsync(ClaimsPrincipal user, SieveModel sieveModel)
    {
        var subClaim = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(subClaim))
        {
            throw new UnauthorizedAccessException("Nie udało się odczytać identyfikatora użytkownika z tokena.");
        }

        if (!Guid.TryParse(subClaim, out var agentId))
        {
            throw new ArgumentException("Nieprawidłowy identyfikator użytkownika w tokenie.");
        }
        
        var query = _context.Leads.Where(lead => lead.AgentId == agentId).AsQueryable();

        query = _sieveProcessor.Apply(sieveModel, query);
        var leads = await query.ToListAsync();

        return _mapper.Map<IEnumerable<LeadDTO>>(leads);
    }
    public async Task<LeadDTO> UpdateLeadStatusAsync(Guid leadId, LeadStatus newStatus, ClaimsPrincipal user, string rejectionReason = null)
    {
        var lead = await _context.Leads.FirstOrDefaultAsync(x => x.LeadId == leadId);
        if (lead == null)
        {
            throw new KeyNotFoundException($"Lead with ID {leadId} was not found.");
        }
        
        var subClaim = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(subClaim))
        {
            throw new UnauthorizedAccessException("Nie udało się odczytać identyfikatora użytkownika z tokena.");
        }

        if (!Guid.TryParse(subClaim, out var agentId))
        {
            throw new ArgumentException("Nieprawidłowy identyfikator użytkownika w tokenie.");
        }
        
        if (lead.AgentId != agentId)
        {
            throw new UnauthorizedAccessException("Agent nie ma uprawnień do zmiany statusu tego leada.");
        }
        
        if (newStatus == LeadStatus.Odrzucony && string.IsNullOrEmpty(rejectionReason))
        {
            throw new ArgumentException("Powód odrzucenia jest wymagany, gdy status leada jest 'Odrzucony'.");
        }
        
        lead.Status = newStatus;
        
        if (newStatus == LeadStatus.Odrzucony)
        {
            lead.RejectionReason = rejectionReason;
        }
        
        _context.Leads.Update(lead);
        await _context.SaveChangesAsync();
        
        var leadDto = _mapper.Map<LeadDTO>(lead);
    
        return leadDto;
    }

}