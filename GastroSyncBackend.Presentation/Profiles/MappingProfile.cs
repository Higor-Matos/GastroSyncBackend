using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Presentation.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MesaEntity, MesaDTO>()
            .ForMember(dest => dest.Consumidores, opt => opt.MapFrom(src => src.Consumidores));
        CreateMap<ConsumidorEntity, ConsumidorDTO>();
        CreateMap<ProdutoEntity, ProdutoDTO>();
        CreateMap<PedidoEntity, PedidoDTO>()
            .ForMember(dest => dest.Divisoes, opt => opt.MapFrom(src => src.Divisoes))
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));  // Novo mapeamento
        CreateMap<DivisaoProdutoEntity, DivisaoProdutoDTO>();
    }
}
