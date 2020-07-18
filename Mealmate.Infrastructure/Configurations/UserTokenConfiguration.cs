﻿using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserToken", "Identity");

            builder.HasKey(u => new { u.LoginProvider, u.UserId, u.Name });

            builder.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserTokens)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
