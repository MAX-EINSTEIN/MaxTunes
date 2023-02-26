namespace MaxTunes
{
    internal abstract class PlaylistInfoRetriever
    {
        protected string ACCESS_TOKEN;
        public PlaylistInfoRetriever(string api_access_token) => ACCESS_TOKEN = api_access_token;

        public abstract Task<string> searchPlaylist(string playlist_id);
    }
}