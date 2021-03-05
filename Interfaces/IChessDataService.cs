using System.Threading.Tasks;

public interface IChessDataService
{
    public Task<string> GetResponse(string username);
}