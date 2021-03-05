using System.Threading.Tasks;
using System.Collections.Generic;
using FirstBlazorApp.Data;

public interface IChessDataService
{
    public Task<List<ChessDataResponse>> GetResponse(string username);
}