using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Abstract;

namespace TaskManagement.Infrastructure.EntityTypeConfiguration
{
    public abstract class BaseEntityCFG<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x=>x.CreateDate).IsRequired();
            builder.Property(x=>x.IsDeleted).IsRequired();

        }
    }
}
