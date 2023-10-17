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


        CreateMap<ConfiguracaoEstabelecimentoEntity, CoverStatusDTO>()
            .ForMember(dest => dest.IsCoverAtivo, opt => opt.MapFrom(src => src.UsarCover))
            .ForMember(dest => dest.ValorCover, opt => opt.MapFrom(src => src.ValorCover));
        CreateMap<PagamentoEntity, PagamentoDetalhadoDto>()
            .ForMember(dest => dest.ConsumidorNome, opt => opt.MapFrom(src => src.Consumidor!.Nome))
            .ForMember(dest => dest.PedidosPagos, opt => opt.MapFrom(src => src.Consumidor!.Pedidos!.Select(p => p.Id.ToString()).ToList()))
            .ForMember(dest => dest.DivisoesProdutos, opt => opt.MapFrom(src => src.Consumidor!.Pedidos!.SelectMany(p => p.Divisoes!)));

        CreateMap<DivisaoProdutoEntity, DivisaoProdutoDTO>()
            .ForMember(dest => dest.ConsumidorId, opt => opt.MapFrom(src => src.ConsumidorId))
            .ForMember(dest => dest.ConsumidorNome, opt => opt.MapFrom(src => src.Consumidor!.Nome))
            .ForMember(dest => dest.ValorDividido, opt => opt.MapFrom(src => src.ValorDividido))
            .ForMember(dest => dest.NomeProduto, opt => opt.MapFrom(src => src.NomeProduto))
            .ForMember(dest => dest.QuantidadeProduto, opt => opt.MapFrom(src => src.QuantidadeProduto))
            .ForMember(dest => dest.TotalDivisoes, opt => opt.MapFrom(src => src.TotalDivisoes));

    }
}

