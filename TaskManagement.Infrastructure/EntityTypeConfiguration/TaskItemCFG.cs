using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagement.Infrastructure.EntityTypeConfiguration
{
    public class TaskItemCFG : BaseEntityCFG<TaskItem>
    {
        override public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasOne(t => t.AppUser)
                    .WithMany()
                    .HasForeignKey(t => t.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(T => T.Title).IsRequired().HasMaxLength(200);
            builder.Property(T => T.Description).HasMaxLength(1000);
            builder.Property(T => T.Priority).IsRequired();
            builder.Property(T => T.Status).IsRequired();
            base.Configure(builder);
        }
    }
}
