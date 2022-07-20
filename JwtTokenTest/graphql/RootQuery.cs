using GraphQL.Types;
using JwtTokenTest.graphql.Users;

namespace JwtTokenTest.graphql
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<UsersQueries>()
                .Name("Users")
                .Resolve(_ => new { });
        }
    }
}