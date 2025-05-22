using GettingStarted.Types;
using HotChocolate.Subscriptions;

namespace GettingStarted;

public class BookAdderBackgroundService
{
    public BookAdderBackgroundService(ITopicEventSender graphEventSender)
    {
        GraphEventSender = graphEventSender;
    }

    private ITopicEventSender GraphEventSender { get; }

    public Task StartAddingBooks()
    {
        return Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(2000);

                // Send the 3 union types
                await GraphEventSender.SendAsync(
                    TestSubscription.BookSubscriptionKey,
                    SciFiAuthorBook()
                );
                await Task.Delay(500);
                await GraphEventSender.SendAsync(
                    TestSubscription.BookSubscriptionKey,
                    NonFictionAuthorBook()
                );
                await Task.Delay(500);
                await GraphEventSender.SendAsync(
                    TestSubscription.BookSubscriptionKey,
                    FictionAuthorBook()
                );
            }
        });
    }

    private Book SciFiAuthorBook()
    {
        return new Book("SciFi Author", new SciFiAuthor() { SpaceName = "Zara Quasar" });
    }

    private Book NonFictionAuthorBook()
    {
        return new Book(
            "Into the wild",
            new NonFictionAuthor()
            {
                RealName = "Jon Krakauer",
                Birthdate = new DateTime(1954, 04, 12),
            }
        );
    }

    private Book FictionAuthorBook()
    {
        return new Book(
            "It",
            new FictionAuthor()
            {
                PenName = "Richard Backman",
                FictionGenre = "I don't know",
                StoryCount = 65,
            }
        );
    }
}
