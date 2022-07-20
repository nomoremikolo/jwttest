using GraphQL;
using GraphQL.Types;
using JwtTokenTest.graphql.Users.entities;
using JwtTokenTest.graphql.Users.types;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenTest.graphql.Users
{
    public class UsersQueries : ObjectGraphType
    {
        public UsersQueries(IHttpContextAccessor accesor)
        {
            Field<NonNullGraphType<LoginType>, LoginEntity>()
                .Name("Login")
                .Argument<NonNullGraphType<StringGraphType>, string>("username", "username")
                .Argument<NonNullGraphType<StringGraphType>, string>("password", "password")
                .Resolve(ctx =>
                {
                    var username = ctx.GetArgument<string>("username");
                    var password = ctx.GetArgument<string>("password");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("secret1234567890");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    //tokenHandler.WriteToken(token);

                    if (username == "mikolo")
                    {
                        if (password == "111")
                        {
                            return new LoginEntity
                            {
                                token = tokenHandler.WriteToken(token)
                            };
                        }
                    }
                    return new LoginEntity
                    {
                        token = "error"
                    };

                });

            Field<StringGraphType>()
                .Name("test")
                .Resolve(ctx =>
                {
                    var value = accesor.HttpContext.User.Claims.First(s => s.Type == "username").Value;
                    return value;
                })
                .AuthorizeWith("Authenticated");
        }
    }
}
