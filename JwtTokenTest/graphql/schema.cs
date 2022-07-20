using GraphQL.Types;

namespace JwtTokenTest.graphql
{
    public class schema : Schema
    {
        public schema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            //Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}
