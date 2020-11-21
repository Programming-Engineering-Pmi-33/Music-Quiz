using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System.Collections.Generic;


namespace SpotifyAPI
{
    class Program
    {
        static async Task<List<string>> GetSongsFromPlaylist(string playlistId) {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("3fd87b1e985c4733b30f6f387138b3ef", "615c188b29534b0c9e2f74c63b93de20");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var playlist = await spotify.Playlists.Get(playlistId);

            List<string> songsIds = new List<string>();

            foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items)
            {
                if (item.Track is FullTrack track)
                {
                    string url = track.PreviewUrl;
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                     songsIds.Add(track.Id);
                    }
                }
            }
            
            return songsIds;
        }



        static async Task Main()
        {
            // Get songs ids from the playlist
            List<string> songsIds = await GetSongsFromPlaylist("37i9dQZF1DX1rVvRgjX59F");

            foreach (var item in songsIds)
            {
                Console.WriteLine(item);
            }
        }

    }
}