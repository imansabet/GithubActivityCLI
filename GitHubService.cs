using System.Text.Json;

public class GitHubService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    public async Task GetUserActivityAsync(string userName)
    {
        var url = $"https://api.github.com/users/{userName}/events";

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpApp");

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error :{response.StatusCode} - {response.ReasonPhrase}");
                return;
            }

            var json = await  response.Content.ReadAsStringAsync();
            var options  = new JsonSerializerOptions
            {
                
                PropertyNameCaseInsensitive = true
            };
            
            var events = JsonSerializer.Deserialize<List<GitHubEvent>>(json, options);
            if (events == null || events.Count == 0)
            {
                Console.WriteLine(" No recent activity found.");
                return;
            }
            foreach (var e in events)
            {
                switch (e.Type)
                {
                    case "PushEvent":
                        Console.WriteLine($" Pushed commits to {e.Repo.Name}");
                        break;
                    case "IssuesEvent":
                        Console.WriteLine($" Issue activity in {e.Repo.Name}");
                        break;
                    case "WatchEvent":
                        Console.WriteLine($" Starred {e.Repo.Name}");
                        break;
                    case "PullRequestEvent":
                        Console.WriteLine($" Pull request in {e.Repo.Name}");
                        break;
                    default:
                        Console.WriteLine($" {e.Type} in {e.Repo.Name}");
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Exception: {ex.Message}");
        }

    }
}