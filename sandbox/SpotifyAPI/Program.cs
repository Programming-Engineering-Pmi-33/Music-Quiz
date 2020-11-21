using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;


namespace SpotifyAPI
{
    class Program
    {
        

        static async Task Main()
        {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("3fd87b1e985c4733b30f6f387138b3ef", "615c188b29534b0c9e2f74c63b93de20");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));



            var track = await spotify.Tracks.Get("0sfdiwck2xr4PteGOdyOfz");
            Console.WriteLine(track.PreviewUrl);
        }

    }
}