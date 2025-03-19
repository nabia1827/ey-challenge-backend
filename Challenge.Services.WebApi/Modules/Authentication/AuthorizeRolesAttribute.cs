using Microsoft.AspNetCore.Authorization;
using static Challenge.Transversal.Common.Constants;

namespace Challenge.Services.WebApi.Modules.Authentication
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
