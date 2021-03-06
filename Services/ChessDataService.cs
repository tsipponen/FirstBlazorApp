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


        public async Task<List<ChessData>> GetResponse(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.chess.com/pub/player/{username}/games");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            request.Headers.Add("Connection", "keep-alive");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var responseAsString = await response.Content.ReadAsStringAsync();

            var listOfModdedData = ParseResponse(responseAsString);

            return listOfModdedData.ToList();
        }

        private IList<ChessData> ParseResponse(string responseAsString)
        {
            JObject jObj = JObject.Parse(responseAsString);
            IList<JToken> results = jObj["games"].Children().ToList();

            IList<ChessDataResponse> listOfGames = new List<ChessDataResponse>();
            IList<ChessData> listOfModdedData = new List<ChessData>();
            foreach (JToken result in results)
            {
                ChessDataResponse singleGame = result.ToObject<ChessDataResponse>();
                listOfModdedData.Add(
                    new ChessData
                    {
                        Turn = singleGame.Turn,
                        White = new WhitePlayer { Url = singleGame.White },
                        Black = new BlackPlayer { Url = singleGame.Black },
                        Fen = singleGame.Fen
                    }
                    );
                listOfGames.Add(singleGame);
            }

            return listOfModdedData;
        }
    }

}