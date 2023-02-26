using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Models; //Models for the JSON-responses

namespace MaxTunes
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

        public override async Task<string> searchPlaylist(string playlist_id)
        {
            try
            {
                FullPlaylist playlist = await _spotify.GetPlaylistAsync(playlist_id);

                // [TODO: Remove this line] Logging Tracks Name
                playlist.Tracks.Items.ForEach(track => Console.WriteLine(track.Track.Name));

                var playlistNameWithCount = $"{playlist.Name} ({playlist.Tracks.Total})";
                return playlistNameWithCount;
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{e.Message}");
            }

            return "ERROR: Playlist not found";
        }
    }
}