using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace Api.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
            new ApplicationRole
            {
                Id = 1,
                Name = "Visitor",
                NormalizedName = "VISITOR"
            },
            new ApplicationRole
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            },
            new ApplicationRole
            {
                Id = 3,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
        }
    }
}
