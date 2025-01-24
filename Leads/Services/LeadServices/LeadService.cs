
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

namespace ApiMikolajekXD.Services;

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

    public async Task<IEnumerable<LeadDTO>> GetLeadsByAgentIdAsync(int agentId, [FromQuery] SieveModel sieveModel)
    {
        var query = _context.Leads.Where(lead => lead.AgentId == agentId).AsQueryable();
        
        query = _sieveProcessor.Apply(sieveModel, query);
        var leads = await query.ToListAsync();

        return _mapper.Map<IEnumerable<LeadDTO>>(leads);
    }

    public async Task<LeadDTOWithAgentDetails> GetLeadById(int id)
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


    public async Task<Lead> RemoveLeadById(int id)
    {
        var removeLead = await _context.Leads.FindAsync(id);
        _context.Leads.Remove(removeLead);
        await _context.SaveChangesAsync();
        return removeLead;
    }
}