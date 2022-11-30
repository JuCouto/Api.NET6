using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // Conexão com banco de dados.
        private readonly ApplicationDbContext _db;

        // Transação. Não tem o readonly, pois assim permite ser iniciada fora do construtor.
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }
        public async Task RollBack()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

    }
}
