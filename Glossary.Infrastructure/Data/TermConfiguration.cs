using Glossary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Infrastructure.Data
{
    public class TermConfiguration : IEntityTypeConfiguration<Term>
    {
        public void Configure(EntityTypeBuilder<Term> b)
        {
            b.ToTable("Terms");

            b.HasKey(x => x.Id);

            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Definition)
                .IsRequired();

            b.Property(x => x.Status)
                .IsRequired();

            b.Property(x => x.AuthorId)
                .IsRequired();

            b.HasIndex(x => x.Name);
        }

    }
}
