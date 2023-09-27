﻿using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IProdutoRepository
{
    Task<List<ProdutoEntity>> GetProdutosAsync();

    Task<List<ProdutoEntity>> GetProdutosByCategoriaAsync(string categoria);
}