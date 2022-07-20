using GraphQL.Types;
using JwtTokenTest.graphql.Users.entities;

namespace JwtTokenTest.graphql.Users.types
{
    public class LoginType : ObjectGraphType<LoginEntity>
    {
        public LoginType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("token")
                .Resolve(ctx => ctx.Source.token);
        }
    }
}
