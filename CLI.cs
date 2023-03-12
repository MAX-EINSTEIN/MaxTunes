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


        private static void setUpHandlersforOptions(string accessToken)
        {
            rootCommand.AddCommand(convertCommand);

            convertCommand.SetHandler(async (sourcePlaylist, sourceService, destService, supportedServices) =>
            {
                if (sourcePlaylist != null)
                {
                    try
                    {
                        Core.SpotifyPlaylistInfoRetriever sp = new Core.SpotifyPlaylistInfoRetriever(accessToken);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Fetching playlist and the contained tracks details from Spotify Web API.\nPlease Wait...\n");

                        var playlist = await sp.searchPlaylist(sourcePlaylist);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"SOURCE PLAYLIST: {playlist.Name}, TOTAL SONGS: {playlist.TotalSongs}\n");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("LISTING ALL TRACKS: \n");

                        var tracks = playlist.getTracks();
                        Boolean toggleColor = true;
                        foreach (var track in tracks)
                        {
                            if (toggleColor) Console.ForegroundColor = ConsoleColor.White;
                            else Console.ForegroundColor = ConsoleColor.DarkGray;

                            toggleColor = !toggleColor;

                            Console.WriteLine("|  TRACK: {0} \n|  ARTIST: {1} \n|  ALBUM: {2}\n",
                                                track.Name,
                                                track.ArtistName,
                                                track.AlbumName);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.White;
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

        public static async Task<int> initializeCLI(string[] args, string accessToken)
        {
            setUpHandlersforOptions(accessToken);
            return await rootCommand.InvokeAsync(args);
        }
    }

}