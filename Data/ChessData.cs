using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirstBlazorApp.Data
{
    public class ChessData : IChessData
    {

        private readonly IHttpClientFactory _clientFactory;
        public string Username { get; set; }

        public string Fen { get; set; }

        public ChessData(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<string> GetResponse()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.chess.com/pub/player/{Username}/games");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Connection", "keep-alive");
            //request.Headers.Add("Content-Type", "application/json");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            Console.WriteLine(response.StatusCode);
            return "moro";
        }
    }

}