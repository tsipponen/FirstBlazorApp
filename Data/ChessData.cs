namespace FirstBlazorApp.Data
{
    public class ChessData
    {

        public string Turn { get; set; }
        public string Fen { get; set; }
        public WhitePlayer White { get; set; }
        public BlackPlayer Black { get; set; }

    }
}