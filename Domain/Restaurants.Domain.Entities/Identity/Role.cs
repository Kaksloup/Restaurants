using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities;

public class Role : IdentityRole<int>
{
}

public class UserClaim : IdentityUserClaim<int>
{
}

public class UserLogin : IdentityUserLogin<int>
{
}

public class UserToken : IdentityUserToken<int>
{
}

public class RoleClaim : IdentityRoleClaim<int>
{
}

public class UserRole : IdentityUserRole<int>
{
}