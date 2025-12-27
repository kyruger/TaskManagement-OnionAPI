using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Concrete;

namespace TaskManagement.Infrastructure.EntityTypeConfiguration
{
    public class RefreshTokenCFG:BaseEntityCFG<RefreshToken>
    {
        override public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(rt=>rt.Token).IsRequired();


            builder.HasOne(rt => rt.AppUser)
                .WithMany(U => U.RefreshTokens)
                .HasForeignKey(rt => rt.AppUserId);


            base.Configure(builder);
        }
    }
}
