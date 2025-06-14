using System;
using System.IO;
using Xunit;

namespace CA1000.Tests;

public class ProgramTests
{
    [Fact]
    public void Main_PrintsGreeting()
    {
        using var sw = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(sw);
        try
        {
            Program.Main(Array.Empty<string>());
        }
        finally
        {
            Console.SetOut(originalOut);
        }
        var output = sw.ToString().Trim();
        Assert.Equal("Hello, World!", output);
    }
}

