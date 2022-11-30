using ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Entities
{
    public sealed class Person
    {
        public int Id { get;private set; }
        public string Name { get;private set; }
        public string Document { get;private set; }
        public string Phone { get;private set; }
        public ICollection<Purchase> Purchases { get; set; }

        // Construtor para adicionar uma pessoa.
        public Person(string document, string name, string phone)
        {
            Validation(document, name, phone);
            Purchases = new List<Purchase>();
        }

        // Construtor para editar uma pessoa.
        public Person(int id,string document, string name, string phone)
        {
            DomainValidationException.When(id < 0, "Id deve ser informado");
            Id = id;
            Validation(document, name, phone);
            Purchases = new List<Purchase>();
        }
        private void Validation(string document, string name, string phone)
        {
            // Validação nulo ou vazio.
            DomainValidationException.When(string.IsNullOrEmpty(name), " Nome deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(document), " Documento deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(phone), " Telefone deve ser informado!");

            Name = name;
            Document = document;
            Phone = phone;
        }
    }
}
