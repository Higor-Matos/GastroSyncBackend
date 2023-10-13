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
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto))
            .ForMember(dest => dest.Divisoes, opt => opt.MapFrom(src => src.Divisoes)); 

        CreateMap<DivisaoProdutoEntity, DivisaoProdutoDTO>();
        CreateMap<ConfiguracaoEstabelecimentoEntity, CoverStatusDTO>()
            .ForMember(dest => dest.IsCoverAtivo, opt => opt.MapFrom(src => src.UsarCover))
            .ForMember(dest => dest.ValorCover, opt => opt.MapFrom(src => src.ValorCover)); // Adicione esta linha
        CreateMap<PagamentoEntity, PagamentoDTO>();

    }
}

