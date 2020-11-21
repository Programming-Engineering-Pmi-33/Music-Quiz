using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI
{
    public class SpotifyProvider
    {
        public string _clientId;
        private string _clientSecret;
        private SpotifyClient _spotify;
        
        public SpotifyProvider()
        {
            _clientId = GetClientId();
            _clientSecret = GetClientSecret();
        }

        private static string GetClientId()
        {
            return "3fd87b1e985c4733b30f6f387138b3ef";
        }

        private static string GetClientSecret()
        {
            return "615c188b29534b0c9e2f74c63b93de20";
        }

        public async Task UpdateToken()
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest(_clientId, _clientSecret);
            var response = await new OAuthClient(config).RequestToken(request);
            _spotify = new SpotifyClient(config.WithToken(response.AccessToken));
        }

        public async Task<List<string>> GetSongsFromPlaylist(string playlistId)
        {
            var playlist = await _spotify.Playlists.Get(playlistId);

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

        public async Task<string> SearchSong(string songName)
        {
            var searchResponse = await _spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, songName));
            return searchResponse.Tracks.Items.First(track => !string.IsNullOrEmpty(track.PreviewUrl)).PreviewUrl;
        }
    }
}