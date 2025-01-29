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
    public async Task<ActionResult> GetLeadsByAgentId(Guid agentId, [FromQuery] SieveModel sieveModel)
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
    public async Task<ActionResult> GetLeadsById(Guid leadId)
    {
        var lead = await _leadService.GetLeadById(leadId);
        if (lead == null) return NotFound();

        return Ok(lead);
    }
    
    [Authorize(Roles = "app-admin")]
    [HttpDelete]
    public async Task<ActionResult> RemoveLeadById(Guid id)
    {
        await _leadService.RemoveLeadById(id);
        return Ok();
    }
    
    [Authorize(Roles = "app-agent")]
    [HttpGet("my-leads")]
    public async Task<ActionResult> GetMyLeads([FromQuery] SieveModel sieveModel)
    {
        try
        {
            var leads = await _leadService.GetLeadsForCurrentAgentAsync(User, sieveModel);
            if (leads == null || !leads.Any())
            {
                return NotFound("Nie znaleziono leadów dla użytkownika.");
            }

            return Ok(leads);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Roles = "app-agent")]
    [HttpPut("leads/{leadId}/status")]
    public async Task<IActionResult> UpdateLeadStatus(Guid leadId, LeadStatus newStatus, [FromBody] string rejectionReason = null)
    {
        try
        {
            var updatedLead = await _leadService.UpdateLeadStatusAsync(leadId, newStatus, User, rejectionReason);
            return Ok(updatedLead);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}