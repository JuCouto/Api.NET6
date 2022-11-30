using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interfaces;
using ApiDotNet6.Domain.Authorization;
using ApiDotNet6.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokengenerator;

        public UserService(IUserRepository userRepository, ITokenGenerator tokengenerator)
        {
            _userRepository = userRepository;
            _tokengenerator = tokengenerator;
        }

        public async Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO)
        {
            // Validar se a DTO tem dados.
            if (userDTO == null)
                return ResultService.Fail<dynamic>("Objeto deve ser informado");

            // Valida os campos.
            var validator = new UserDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<dynamic>("Problemas com a validação!!", validator);

            // Checar se usuario e senha estão salvos no BD.
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultService.Fail<dynamic>("Usuário ou senha não encontrado!!");

            return ResultService.Ok(_tokengenerator.Generator(user));
         }
    }
}
