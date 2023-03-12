namespace MaxTunes
{
    namespace Core
    {
        internal class Track
        {
            private string name;

            private string artistName;

            private string albumName;

            public string Name { get => name; set => name = value; }
            public string ArtistName { get => artistName; set => artistName = value; }
            public string AlbumName { get => albumName; set => albumName = value; }


            public Track(string t_name, string t_artistName, string t_albumName)
            {
                name = t_name;
                artistName = t_artistName;
                albumName = t_albumName;
            }
        }
    }
}