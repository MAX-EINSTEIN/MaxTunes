using MaxTunes;

class Program
{
    static async Task<int> Main(string[] args)
    {
        // [TODO: Extract this to a config file]
        // [NOTE: Allowing git push for now as it expires in 1 hour]
        const string SPOTIFY_OAUTH_TOKEN = "BQBer-SsVaIxhAEeErCgM1wpVL5n9nFpmm_Iejhs2KmHt71tGlYyrWbHu5gpH1Jrd6gix068oijwwRQtILkgLvCeuFzQeoHZgYGkH3AnD-h6HJktzyL2JkbHqz5peNF0IG10yo0LVEG5UtBsWhjRkgo4uhMC_ztrblBdBqA2zenReAiCVfz8LMaaYqwxUKkS";

        return await CLI.initializeCLI(args, SPOTIFY_OAUTH_TOKEN);
    }

}