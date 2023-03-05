using System.Collections.Generic;
using System.Text.Json;

namespace MaxTunes
{
    namespace Core
    {
        internal class Playlist
        {
            private string name;
            private string description = string.Empty;
            private List<Track> tracks;

            public Playlist(string name, string description)
            {
                this.name = name;
                this.description = description;
                this.tracks = new List<Track>();
            }

            public string Name { get => name; set => name = value; }
            public string Description { get => description; set => description = value; }
            public int TotalSongs { get => tracks.Count; }

            public void addTrack(Track t) => this.tracks.Add(t);

            public List<Track> getTracks() => tracks;

            public string ToJson() => JsonSerializer.Serialize(this);
        }
    }
}