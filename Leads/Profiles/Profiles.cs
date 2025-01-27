using Leads.DTOs.AgentDTOs;
using Leads.DTOs.LeadDTOs;
using Leads.Models;

namespace Leads.Profiles;

public class Profiles : AutoMapper.Profile
{
    public Profiles()
    {
        CreateMap<Agent, AgentDTO>();
        CreateMap<AgentDTO, Agent>();
        CreateMap<Agent, AgentUpdateDTO>();
        CreateMap<AgentUpdateDTO, Agent>();
        CreateMap<Lead, LeadDTOWithAgentDetails>();
        CreateMap<Lead, LeadCreateDTO>();
        CreateMap<Lead, LeadDTO>();
        CreateMap<LeadCreateDTO, Lead>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) // Automatyczne ustawienie CreatedAt
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(1))) // Automatyczne ustawienie ExpiresAt
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => LeadStatus.Nowy)) // Automatyczne ustawienie Status
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => "")); // Automatyczne ustawienie RejectionReason
    }
    
}