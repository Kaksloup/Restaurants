using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Database;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity => { entity.ToTable("Users"); });

        builder.Entity<Role>(entity => { entity.ToTable("Roles"); });

        builder.Entity<UserClaim>(entity => { entity.ToTable("UserClaims"); });

        builder.Entity<UserLogin>(entity => { entity.ToTable("UserLogins"); });

        builder.Entity<UserToken>(entity => { entity.ToTable("UserTokens"); });

        builder.Entity<UserRole>(entity => { entity.ToTable("UserRoles"); });

        builder.Entity<RoleClaim>(entity => { entity.ToTable("RoleClaims"); });
    }
}