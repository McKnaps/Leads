using System.Collections;
using AutoMapper;
using Leads.DTOs.AgentDTOs;
using Leads.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "app-admin")]
    [HttpGet]
    public async Task<IActionResult> GetAgents([FromQuery] SieveModel sieveModel)
    {
        var agents = await _agentService.GetAgentsAsync(sieveModel);
        return Ok(agents);
    }
    [Authorize(Roles = "app-admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAgentById(Guid id)
    {
        var agent = await _agentService.GetAgentByIdAsync(id);
        if (agent == null) return NotFound();

        return Ok(agent);
    }
    [Authorize(Roles = "app-admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAgent(AgentDTO agentDTO)
    {
        return Ok(await _agentService.CreateAgentAsync(agentDTO));
    }
    [Authorize(Roles = "app-admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAgent(Guid id, AgentUpdateDTO agentUpdateDto)
    {
        var updatedAgent = await _agentService.UpdateAgentAsync(id, agentUpdateDto);
        if (updatedAgent == null) return NotFound();

        return NoContent();
    }
    [Authorize(Roles = "app-admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgent(Guid id)
    {
        var result = await _agentService.DeleteAgentAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
    [Authorize(Roles = "app-admin")]
    [HttpGet("by-email")]
    public async Task<IActionResult> GetAgentByEmail([FromQuery] string email)
    {
        var agent = await _agentService.GetAgentByEmailAsync(email);
        if (agent == null) return NotFound();
        return Ok(agent);
    }
}