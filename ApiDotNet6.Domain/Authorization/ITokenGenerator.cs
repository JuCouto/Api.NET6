using ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Authorization
{
    public interface ITokenGenerator
    {
        dynamic Generator(User user);
    }
}
