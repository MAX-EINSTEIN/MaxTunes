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

                    spotifyPlaylist.Tracks.Items.ForEach((track) =>
                    {
                        var fullTrack = _spotify.GetTrack(track.Track.Id);

                        // [TODO]: Replace placeholder <ARTIST_NAME> with real artist name 
                        var t = new Track(track.Track.Name, "<ARTIST_NAME>", fullTrack.Album.Name);
                        playlist.addTrack(t);
                    });

                    return playlist;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine($"{e.Message}");
                }

                throw new System.Exception("ERROR: Playlist not found");
            }
        }
    }
}