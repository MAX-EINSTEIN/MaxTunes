using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Models; //Models for the JSON-responses

namespace MaxTunes
{
    namespace Core
    {
        internal class SpotifyPlaylistInfoRetriever : PlaylistInfoRetriever
        {
            private SpotifyWebAPI _spotify;
            public SpotifyPlaylistInfoRetriever(string api_access_token) : base(api_access_token)
            {
                _spotify = new SpotifyWebAPI()
                {
                    AccessToken = this.ACCESS_TOKEN,
                    TokenType = "Bearer"
                };
            }

            public override async Task<Playlist> searchPlaylist(string playlist_id)
            {
                try
                {
                    FullPlaylist spotifyPlaylist = await _spotify.GetPlaylistAsync(playlist_id);
                    playlist = new Playlist(spotifyPlaylist.Name, spotifyPlaylist.Description);

                    var fetchFullTracksTasks = new List<Task<FullTrack>>();
                    foreach (var track in spotifyPlaylist.Tracks.Items)
                    {
                        fetchFullTracksTasks.Add(_spotify.GetTrackAsync(track.Track.Id));
                    }

                    await Task.WhenAll(fetchFullTracksTasks);

                    foreach (var fullTrackTask in fetchFullTracksTasks)
                    {
                        var fullTrack = fullTrackTask.Result;
                        // [TODO]: Replace placeholder <ARTIST_NAME> with real artist name 
                        var track = new Track(fullTrack.Name, "<ARTIST_NAME>", fullTrack.Album.Name);
                        playlist.addTrack(track);
                    }

                    return playlist;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: {e.Message}");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("INFO: Possible Fix - Refetch the access token.");

                    Console.ForegroundColor = ConsoleColor.White;
                }

                throw new System.Exception("ERROR: Playlist not found. Please enter a valid public playlist id.");
            }
        }
    }
}