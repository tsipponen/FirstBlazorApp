using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FirstBlazorApp.Data
{
    public class ChessData : IChessData
    {

        private readonly IHttpClientFactory _clientFactory;

        public ChessData(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<string> GetResponse(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.chess.com/pub/player/{username}/games");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Connection", "keep-alive");            
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var asd = await response.Content.ReadAsStringAsync();
            return asd;
        }
    }

}