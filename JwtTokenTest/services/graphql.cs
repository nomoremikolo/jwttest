using GraphQL;
using GraphQL.Server;
using JwtTokenTest.graphql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtTokenTest.services
{
    public static class graphql
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            
            services.AddSingleton<schema>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = (context) =>
                {
                    Console.WriteLine("StackTrace:" + context.Exception.StackTrace);
                    Console.WriteLine("Message:" + context.Exception.Message);
                    context.ErrorMessage = context.Exception.Message;
                };
            })
                .AddSystemTextJson()
                .AddGraphTypes(typeof(schema))
                .AddGraphQLAuthorization(options =>
                {
                    options.AddPolicy("Authenticated", p => p.RequireAuthenticatedUser());
                });

            services.AddSingleton<RootQuery>();
            services.AddSingleton<RootMutations>();
            return services;
        }
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret1234567890")),
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
            return services;
        }
    }
}
