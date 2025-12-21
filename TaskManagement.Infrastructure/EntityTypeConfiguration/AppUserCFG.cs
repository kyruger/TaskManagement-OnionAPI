using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagement.Infrastructure.EntityTypeConfiguration
{
    public class AppUserCFG:BaseEntityCFG<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(U=>U.Tasks)
                   .WithOne(T=>T.AppUser)
                   .HasForeignKey(T=>T.AppUserId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            base.Configure(builder);
        }
    }
}
