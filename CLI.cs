using System.CommandLine;

namespace MaxTunes
{
    internal static class CLI
    {
        const string name = "MaxTunes";
        const string description = "Convert your favorite playlists from one music streaming service to another";

        // Options
        static Option<string?> sourcePlaylistOption = new Option<string?>(
            name: "--source-playlist",
            description: "The playlist of any supported music streaming service");

        static Option<string?> sourceServiceOption = new Option<string?>(
            name: "--source-service",
            description: "The name of the music streaming service of the source playlist");

        static Option<string?> destinationServiceOption = new Option<string?>(
            name: "--dest-service",
            description: "The name of the music streaming service you want to convert the playlist into"
            );

        static Option<bool> supportedServicesOption = new Option<bool>(
            name: "--supported-services",
            description: "The names of the streaming services for which conversion is possible"
            );

        // Commands
        static RootCommand rootCommand = new RootCommand($"{name} - {description}");

        static Command convertCommand = new Command("convert", "Convert a playlist from a music streaming service (source service) to another service (destination service)")
        {
            sourcePlaylistOption,
            sourceServiceOption,
            destinationServiceOption,
            supportedServicesOption
        };


        private static void setUpHandlersforOptions()
        {
            rootCommand.AddCommand(convertCommand);

            convertCommand.SetHandler(async (sourcePlaylist, sourceService, destService, supportedServices) =>
            {
                if (sourcePlaylist != null)
                {
                    // [TODO: Extract this to a config file]
                    // [NOTE: Allowing git push for now as it expires in 1 hour]
                    const string SPOTIFY_OAUTH_TOKEN = "BQBrTAXrs-VWL-12U_FoQI9eew29duVfM2wzUw5PC6cxx51p8t6q7ClXaDbzA8cR7Uyb0lHru8Ob0kjlKrrAOz-CCWv5iBO2pZirQGS5cbjQLmWw-szsTEebD3xYzxVZhU2GG66U2Sf92mZQXpVL3jC13Bd2nSHAZEBD3xD4Nt--7WY3lwvlcnN9pmNPa3YjxM3D";

                    try
                    {
                        Core.SpotifyPlaylistInfoRetriever sp = new Core.SpotifyPlaylistInfoRetriever(SPOTIFY_OAUTH_TOKEN);
                        var playlistName = await sp.searchPlaylist(sourcePlaylist);

                        Console.WriteLine($"Source Playlist: {playlistName}");
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }


                if (sourceService != null)
                    Console.WriteLine($"Source Service: {sourceService}");

                if (destService != null)
                    Console.WriteLine($"Destination Service: {destService}");

                if (supportedServices)
                {
                    Console.WriteLine(
                        "Supported Services:\r\n" +
                        "1) YouTube Music (yt)\r\n" +
                        "2) iTunes (it)\r\n" +
                        "3) Amazon Music (am)\r\n" +
                        "4) Spotify (sp)\r\n" +
                        "5) SoundCloud (sc)\r\n");
                }
            }, sourcePlaylistOption, sourceServiceOption, destinationServiceOption, supportedServicesOption);
        }

        public static async Task<int> initializeCLI(string[] args)
        {
            setUpHandlersforOptions();
            return await rootCommand.InvokeAsync(args);
        }
    }

}