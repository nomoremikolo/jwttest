using JwtTokenTest.graphql;
using JwtTokenTest.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddGraphQLApi();
builder.Services.AddJwtAuthorization();
builder.Services.AddAuthorization();

var app = builder.Build();

var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();

app.UseGraphQLAltair();
app.UseGraphQL<schema>();

app.Run();
