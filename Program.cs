class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(" Please provide a GitHub username.");
            Console.WriteLine("Usage: github-activity <username>");
            return;
        }

        string username = args[0];
        var service = new GitHubService();
        await service.GetUserActivityAsync(username);
    }
}