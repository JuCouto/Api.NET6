using ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Entities
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodErp { get; set; }
        public decimal Price { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

        public Product(string name, string codErp, decimal price)
        {
            Validation(name, codErp, price);
            Purchases = new List<Purchase>();
        }
        public Product(int Id, string name, string codErp, decimal price)
        {
            DomainValidationException.When(Id <0, "Id do produto deve ser informado!");
            Id = Id;
            Validation(name, codErp, price);
            Purchases = new List<Purchase>();
        }
        private void Validation(string name, string codErp, decimal price)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(codErp), "Código erp deve ser informado!");
            DomainValidationException.When(price < 0, "Preço deve ser informado!");

            Name = name;
            CodErp = codErp;
            Price = price;
        }
    }
}
