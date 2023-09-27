using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Presentation.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MesaEntity, MesaDTO>();
        CreateMap<ConsumidorEntity, ConsumidorDTO>();
    }
}

