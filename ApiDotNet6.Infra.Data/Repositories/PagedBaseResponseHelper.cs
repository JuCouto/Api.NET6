using ApiDotNet6.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Repositories
{
    // Faz a paginação, e vai devolver os dados paginados para o repositório.
    public static class PagedBaseResponseHelper
    {
        // Resposta e entrada dinâmicas (representadas pelo T).
        public static async Task<TResponse> GetResponseAsync<TResponse, T>(IQueryable<T> query, PagedBaseRequest request)
            where TResponse : PagedBaseResponse<T>, new()
        {
            var response = new TResponse();
            var count = await query.CountAsync(); // Busca o total de dados.

            // Converte para int, vai pegar o valor absoluto(abs), count dividido pelo total de páginas.
            response.TotalPages = (int)Math.Abs((double)count / request.PageSize);
            response.TotalRegisters = count;
            // Para ordenar o campo que está sendo recebido criou o método abaixo
            if (string.IsNullOrEmpty(request.OrderByProperty))
                response.Data = await query.ToListAsync();
            else
                response.Data = query.OrderByDynamic(request.OrderByProperty) // Ordenou as páginas
                    .Skip(request.Page - 1 * request.PageSize) // Vai pular as páginas,começando da página 0.
                    .Take(request.PageSize) // Vai pegar as 10 primeiras linhas.
                    .ToList();

            return response;
        }
        private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
        {
            // Vai ordernar de acordo com o dado informado.
            // Informar a propriedade que está na entidade.
            return query.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }
    }
}
