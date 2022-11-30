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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        // Mapea os campos do banco para a entidade.
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Nome da tabela.
            builder.ToTable("usuario");

            // Chave Primária.
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("idusuario");

            builder.Property(u => u.Email)
                .HasColumnName("email");

            builder.Property(u => u.Password)
                .HasColumnName("senha");
        }
    }
}
