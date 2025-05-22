namespace GettingStarted.Types;

public interface IAuthor { }

public class SciFiAuthor : IAuthor
{
    public required string SpaceName { get; set; }
}

public class NonFictionAuthor : IAuthor
{
    public required string RealName { get; set; }
    public required DateTime Birthdate { get; set; }
}

public class FictionAuthor : IAuthor
{
    public required string PenName { get; set; }
    public required string FictionGenre { get; set; }
    public required int StoryCount { get; set; }
}
