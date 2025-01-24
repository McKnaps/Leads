using Leads.DTOs.LeadDTOs;
using Leads.Services;
using Leads.Services.LeadServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Leads.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeadController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadController(ILeadService leadService)
    {
        _leadService = leadService;
    }
    [Authorize(Roles = "app-admin")]
    [HttpGet("agent/{agentId}")]
    public async Task<ActionResult> GetLeadsByAgentId(int agentId, [FromQuery] SieveModel sieveModel)
    {
        var leads = await _leadService.GetLeadsByAgentIdAsync(agentId, sieveModel);

        if (leads == null || !leads.Any()) 
            return NotFound();

        return Ok(leads);
    }
    

    [Authorize(Roles = "app-admin")]
    [HttpPost]
    public async Task<ActionResult> CreateLead(LeadCreateDTO leadCreateDto)
    {
        var lead = await _leadService.CreateLeadAsync(leadCreateDto);
        if (lead == null) return BadRequest("Nie udało się utworzyć leada");

        return CreatedAtAction(nameof(GetLeadsById), new { leadId = lead.LeadId }, lead);
    }
    
    [Authorize(Roles = "app-admin")]
    [HttpGet("lead/{leadId}")]
    public async Task<ActionResult> GetLeadsById(int leadId)
    {
        var lead = await _leadService.GetLeadById(leadId);
        if (lead == null) return NotFound();

        return Ok(lead);
    }
    
    [Authorize(Roles = "app-admin")]
    [HttpDelete]
    public async Task<ActionResult> RemoveLeadById(int id)
    {
        await _leadService.RemoveLeadById(id);
        return Ok();
    }
}