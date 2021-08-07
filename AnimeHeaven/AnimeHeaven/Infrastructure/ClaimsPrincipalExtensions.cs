namespace AnimeHeaven.Infrastructure
{
    using System;
    using System.Security.Claims;

    using static AnimeHeaven.Areas.Admin.AdminConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            if (user.Identity.Name == null)
            {
                return null;
            }

            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
