using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Services.Interfaces.Mapping;

namespace GastroSyncBackend.Services.Implementations.Mapping;

public class MapToDtoService : IMapToDtoService
{
    public async Task<MesaDto> MapMesaToDtoAsync(MesaEntity mesaEntity)
    {
        await Task.Yield();

        return new MesaDto
        {
            Id = (int)mesaEntity.Id,
            NumeroMesa = (int)mesaEntity.NumeroMesa,
            Local = mesaEntity.Local!,
            Consumidores = mesaEntity.Consumidores!.Select(c => new ConsumidorDto
            {
                Id = (int)c.Id,
                Nome = c.Nome!
            }).ToList()
        };
    }

    public async Task<ConsumidorDto> MapConsumidorToDtoAsync(ConsumidorEntity consumidorEntity)
    {
        await Task.Yield();

        return new ConsumidorDto
        {
            Id = (int)consumidorEntity.Id,
            Nome = consumidorEntity.Nome!
        };
    }
}