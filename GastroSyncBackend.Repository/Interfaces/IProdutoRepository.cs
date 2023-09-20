﻿using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;
[AutoDI]
public interface IProdutoRepository
{
    List<Produto> GetAllProdutos();
    Produto GetProdutoById(int id);
}