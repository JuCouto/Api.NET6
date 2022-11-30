using ApiDotNet6.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Filters
{
    public class PersonFilterDb : PagedBaseRequest
    {
        // Pasta de filtros que serão usados nos repositórios, serviços e controles.
        public string? Name { get; set; }
    }
}
