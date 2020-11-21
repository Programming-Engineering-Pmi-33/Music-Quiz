using System;
using System.Threading.Tasks;

namespace SpotifyAPI
{
    class Program
    {
        static async Task Main()
        {
            // Create SpotifyProvider instance
            var spotifyProvider = new SpotifyProvider();
            await spotifyProvider.UpdateToken();
            
            // Get songs ids from the playlist
            var songsIds = await spotifyProvider.GetSongsFromPlaylist("37i9dQZF1DX1rVvRgjX59F");

            foreach (var songId in songsIds)
            {
                Console.WriteLine(songId);
            }
            Console.WriteLine("Search");
            var searchResult = await spotifyProvider.SearchSong("Green Day - Holiday");
            Console.WriteLine(searchResult);
        }
    }
}