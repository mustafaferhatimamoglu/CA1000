using System;
using System.IO;

namespace CA1000.Tests;

public class ProgramTests
{
    [Fact]
    public async Task Main_PrintsGreeting()
    {
        Environment.SetEnvironmentVariable("SKIP_OPENAI", "1");
        using var sw = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(sw);
        try
        {
            await Program.Main(Array.Empty<string>());
        }
        finally
        {
            Console.SetOut(originalOut);
            Environment.SetEnvironmentVariable("SKIP_OPENAI", null);
        }
        var output = sw.ToString().Trim();
        Assert.Equal("Merhaba\nMerhaba!", output);
    }
}

