using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NuGet.Protocol;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace SynetraApi.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomSwaggerFilter(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
           
            string roleUser = string.Empty;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                roleUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            }

            List<string> pathToRemove = new List<string>();
            foreach (var item in context.ApiDescriptions)
            {
                var rolesFromAttribute = item.CustomAttributes()
                    .OfType<AuthorizeAttribute>()
                    .Select(x => x.Roles)
                    .Distinct();

                if (rolesFromAttribute.Any())
                {
                    string? roleAttribute = rolesFromAttribute.FirstOrDefault();
                    if (roleAttribute != null)
                    {
                        string[] roles = roleAttribute.Split(',');
                        if (!roles.Contains(roleUser))
                        {
                            pathToRemove.Add("/" + item.RelativePath);
                        }
                    }
                }
            }
            pathToRemove.ForEach(x => { swaggerDoc.Paths.Remove(x); });
        }

    }
}
