
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.IO;

namespace CA1000;

public static class Program
{
    // The application looks for the API key in the OPENAI_API_KEY environment
    // variable. If it is not set, it will read the file specified by
    // OPENAI_API_KEY_FILE or "openai_key.txt" in the working directory.
    private const string DefaultKeyFile = "openai_key.txt";

    public static async Task Main(string[] args)
    {
        // Skip calling the API when running tests.
        var skipApi = Environment.GetEnvironmentVariable("SKIP_OPENAI") == "1";
        var key = LoadApiKey();

        string greeting;
        if (!skipApi && !string.IsNullOrEmpty(key))

        {
            greeting = await GetGreetingAsync(key) ?? "Merhaba!";
        }
        else
        {
            greeting = "Merhaba!";
        }

        Console.WriteLine(greeting);
    }

    private static string? LoadApiKey()
    {
        var key = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (!string.IsNullOrEmpty(key))
        {
            return key;
        }

        var file = Environment.GetEnvironmentVariable("OPENAI_API_KEY_FILE") ?? DefaultKeyFile;
        if (File.Exists(file))
        {
            return File.ReadAllText(file).Trim();
        }

        return null;
    }

    private static async Task<string?> GetGreetingAsync(string apiKey)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var request = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = "Merhaba" }
            }
        };

        var json = JsonSerializer.Serialize(request);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);
        var root = doc.RootElement;
        if (root.TryGetProperty("choices", out var choices) &&
            choices.GetArrayLength() > 0)
        {
            var message = choices[0].GetProperty("message").GetProperty("content").GetString();
            return message;
        }
        return null;
    }
}
