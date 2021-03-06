using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace FirstBlazorApp.Data
{
    public class ChessDataService : IChessDataService
    {

        private readonly IHttpClientFactory _clientFactory;

        public ChessDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<List<ChessDataResponse>> GetResponse(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.chess.com/pub/player/{username}/games");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Connection", "keep-alive");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var responseAsString = await response.Content.ReadAsStringAsync();
            JObject jObj = JObject.Parse(responseAsString);
            IList<JToken> results = jObj["games"].Children().ToList();

            IList<ChessDataResponse> listOfGames = new List<ChessDataResponse>();
            foreach (JToken result in results)
            {
                ChessDataResponse singleGame = result.ToObject<ChessDataResponse>();
                listOfGames.Add(singleGame);
            }
            var turn = responseAsString.Where(t => t.Equals("turn")).LastOrDefault();
            return listOfGames.ToList();
        }
    }

}