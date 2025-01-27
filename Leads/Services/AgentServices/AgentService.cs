using AutoMapper;
using Leads.Data;
using Leads.DTOs.AgentDTOs;
using Leads.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Leads.Services.AgentServices;

public class AgentService : IAgentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly SieveProcessor _sieveProcessor;

    public AgentService(ApplicationDbContext context, IMapper mapper, SieveProcessor sieveProcessor)
    {
        _context = context;
        _mapper = mapper;
        _sieveProcessor = sieveProcessor;
    }


    public async Task<IEnumerable<AgentDTO>> GetAgentsAsync(SieveModel sieveModel)
    {
        var agentsQuery = _context.Agents.AsQueryable();
        
        agentsQuery = _sieveProcessor.Apply(sieveModel, agentsQuery);

        var agents = await agentsQuery.ToListAsync();
        return _mapper.Map<IEnumerable<AgentDTO>>(agents);
    }

    public async Task<AgentDTO> GetAgentByIdAsync(Guid id)
    {
        var agent = await _context.Agents.FindAsync(id);
        return _mapper.Map<AgentDTO>(agent);
    }

    public async Task<Agent> CreateAgentAsync(AgentCreateDTO agentCreateDto)
    {
        var agent = _mapper.Map<Agent>(agentCreateDto);

        await _context.Agents.AddAsync(agent);
        await _context.SaveChangesAsync();

        return agent;
    }

    public async Task<Agent> UpdateAgentAsync(Guid id, AgentCreateDTO agentCreateDto)
    {
        var existingAgent = await _context.Agents.FindAsync(id);
        if (existingAgent == null) return null;

        _mapper.Map(agentCreateDto, existingAgent);

        await _context.SaveChangesAsync();

        return existingAgent;
    }

    public async Task<bool> DeleteAgentAsync(Guid id)
    {
        var agent = await _context.Agents.FindAsync(id);
        if (agent == null) return false;

        _context.Agents.Remove(agent);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<AgentDTO> GetAgentByEmailAsync(string email)
    {
        var agent = await _context.Agents.FirstOrDefaultAsync(a => a.Email == email);
        if (agent == null)
        {
            throw new InvalidOperationException("Not Found");
        }

        return _mapper.Map<AgentDTO>(agent);
    }
}