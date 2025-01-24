using System.Collections;
using AutoMapper;
using Leads.DTOs.AgentDTOs;
using Leads.Services;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;

namespace Leads.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly IAgentService _agentService;

    public AgentsController(IAgentService agentService)
    {
        _agentService = agentService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAgents([FromQuery] SieveModel sieveModel)
    {
        var agents = await _agentService.GetAgentsAsync(sieveModel);
        return Ok(agents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAgentById(int id)
    {
        var agent = await _agentService.GetAgentByIdAsync(id);
        if (agent == null) return NotFound();

        return Ok(agent);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAgent(AgentCreateDTO agentCreateDto)
    {
        return Ok(await _agentService.CreateAgentAsync(agentCreateDto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAgent(int id, AgentCreateDTO agentCreateDto)
    {
        var updatedAgent = await _agentService.UpdateAgentAsync(id, agentCreateDto);
        if (updatedAgent == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgent(int id)
    {
        var result = await _agentService.DeleteAgentAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }

    [HttpGet("by-email")]
    public async Task<IActionResult> GetAgentByEmail([FromQuery] string email)
    {
        var agent = await _agentService.GetAgentByEmailAsync(email);
        if (agent == null) return NotFound();
        return Ok(agent);
    }
}