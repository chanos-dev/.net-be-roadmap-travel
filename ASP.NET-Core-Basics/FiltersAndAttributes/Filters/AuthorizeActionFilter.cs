using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FiltersAndAttributes.Filters
{
    public class AuthorizeActionFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public AuthorizeActionFilter(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("AuthorizeActionFilter OnAuthorization");
            bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user, string permission)
            => permission == "read";
    }
}
