# CA1000

This repository contains a small example console application for .NET.

## Building

1. Install the .NET 8 SDK.
2. Run `dotnet build` from the repository root.

## Testing

Run unit tests with:

```bash
dotnet test
```

## Running

After building, you can execute the application with:

```bash
dotnet run
```

The program prints the prompt **Merhaba** and then the response. Without an API key it prints **Merhaba!** as the response.

### Using the OpenAI API

Create a file named `openai_key.txt` containing your API key or set the
`OPENAI_API_KEY` environment variable. The optional variable
`OPENAI_API_KEY_FILE` can point to a different key file. When a key is
available, the program sends a request to the OpenAI chat completion API asking
it to respond to "Merhaba". If the API call fails or `SKIP_OPENAI` is set to
`1`, the program falls back to the local greeting.

```bash
echo YOUR_API_KEY > openai_key.txt
dotnet run
```
