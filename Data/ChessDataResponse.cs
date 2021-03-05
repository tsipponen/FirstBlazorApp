using System;

namespace FirstBlazorApp.Data
{
    public class ChessDataResponse
    {

        public string Turn { get; set; }
        public string Fen { get; set; }
        public string White { get; set; }
        public string Black { get; set; }

    }
}