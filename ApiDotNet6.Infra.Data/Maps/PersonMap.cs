using ApiDotNet6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Maps
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Mapeamento tabela pessoa.
            builder.ToTable("pessoa");
            // Informando a chave primária.
            builder.HasKey(c => c.Id);

            // relacionando o atributo a tabela do banco.
            builder.Property(c => c.Id).HasColumnName("idpessoa").UseIdentityColumn();

            builder.Property(c => c.Document).HasColumnName("documento");

            builder.Property(c => c.Name).HasColumnName("nome");

            builder.Property(c => c.Phone).HasColumnName("celular");

            // Mapeando Chave estrangeira(tipo de relacionamento).
            builder.HasMany(c => c.Purchases).WithOne(p => p.Person).HasForeignKey(c => c.PersonId);
        }
    }
}
