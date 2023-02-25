using MaxTunes;

class Program
{
    static async Task<int> Main(string[] args)
    {
        return await CLI.initializeCLI(args);
    }

}