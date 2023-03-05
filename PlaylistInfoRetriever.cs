namespace MaxTunes
{
    namespace Core
    {
        internal abstract class PlaylistInfoRetriever
        {
            protected string ACCESS_TOKEN;
            protected Playlist? playlist = null;
            public PlaylistInfoRetriever(string api_access_token) => ACCESS_TOKEN = api_access_token;

            public abstract Task<Playlist> searchPlaylist(string playlist_id);
        }
    }

}