using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces.Mapping;

[AutoDI]
public interface IMapToDtoService
{
    Task<MesaDto> MapMesaToDtoAsync(MesaEntity mesaEntity);
    Task<ConsumidorDto> MapConsumidorToDtoAsync(ConsumidorEntity consumidorEntity);
}

