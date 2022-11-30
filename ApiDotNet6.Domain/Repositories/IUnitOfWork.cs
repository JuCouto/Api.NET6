using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Repositories
{
    // Implementar o IDisposable para garantir que uma transação aberta vai ser encerrada, caso algo dê errado.
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task RollBack();
    }
}
