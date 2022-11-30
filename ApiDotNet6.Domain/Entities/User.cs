﻿using ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Entities
{
    public class User
    {

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(string email, string password)
        {
            Validation(email, password);
        }

        public User(int id, string email, string password)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado.");
            Id = id;
           Validation(email, password);
        }

        private void Validation(string email, string password)
        {
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email deve ser informado.");
            DomainValidationException.When(string.IsNullOrEmpty(password), "Senha deve ser informada.");

            Email = email;
            Password = password;
        }
    }
}