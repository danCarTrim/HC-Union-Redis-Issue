using GettingStarted.Types;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = await ConnectionMultiplexer.ConnectAsync("<your con string>");

// Add the IAuthor union type + related types.
// IAuthor, sciFiAuthor, nonFictionAuthor, fictionAuthor are in Author.cs
// Uncomment AddRedisSubscriptions and commend InMemory to test the issue.
builder
    .AddGraphQL()
    //.AddRedisSubscriptions((sp) => redisConnectionString)
    .AddInMemorySubscriptions()
    .AddUnionType<IAuthor>()
    .AddType<FictionAuthor>()
    .AddType<NonFictionAuthor>()
    .AddType<SciFiAuthor>()
    .AddQueryType<Query>()
    .AddSubscriptionType<Subscription>()
    .AddType<TestSubscription>();

// Add out book adder which will send 3 books with different union authors.
builder.Services.AddSingleton<BookAdderBackgroundService>();
var app = builder.Build();
app.UseWebSockets();

// Start our background service and start the app.
var backgroundRunner = app.Services.GetRequiredService<BookAdderBackgroundService>();
var task = backgroundRunner.StartAddingBooks();

app.MapGraphQL();
app.RunWithGraphQLCommands(args);
