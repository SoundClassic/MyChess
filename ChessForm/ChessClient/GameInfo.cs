using System.Collections.Specialized;

namespace ChessForm.ChessClient
{
    public struct GameInfo
    {
        public int GameID { get; private set; }
        public string Fen { get; private set; }
        public string Status { get; private set; }

        public GameInfo(NameValueCollection list)
        {
            GameID = int.Parse(list.Get("ID"));
            Fen = list.Get("FEN");
            Status = list.Get("Status");
        }

        public override string ToString()
        {
            return string.Format("GameID = {0:d}" +
                                "\nFen = {1}" +
                                "\nStatus = {2}",
                                GameID, Fen, Status);
        }
    }
}
