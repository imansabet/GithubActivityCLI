public class GitHubEvent
{
    public string Type { get; set; }
    public Repo Repo { get; set; }

}

public class Repo
{
    public string Name { get; set; }
}