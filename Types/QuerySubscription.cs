using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GettingStarted.Types;

public class Query
{
    public Book GetBook() =>
        new Book(
            "C# in depth.",
            new NonFictionAuthor()
            {
                RealName = "Jon Skeet",
                Birthdate = new DateTime(2000, 07, 01),
            }
        );
}

public class Subscription { }

[ExtendObjectType(typeof(Subscription))]
public class TestSubscription
{
    public static readonly string BookSubscriptionKey = "book_was_added";

    [Subscribe(With = nameof(SubscribeToEvents))]
    public Book BookWasAdded([EventMessage] Book ev) => ev;

    [Subscribe]
    public async Task<ISourceStream<Book>> SubscribeToEvents(ITopicEventReceiver receiver)
    {
        return await receiver.SubscribeAsync<Book>(BookSubscriptionKey);
    }
}
