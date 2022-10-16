using System.Linq;
using MyChess.ChessGame;

namespace ChessAPI.Models
{
    public static class Logic
    {
        public static Game GetCurrentGame()
        {
            using(var db = new ChessContext())
            {
                Game game = db
                            .Games
                            .Where(x => x.Status == "play")
                            .OrderBy(x => x.Id)
                            .FirstOrDefault();
                if (game == null)
                {
                    game = CreateNewGame();
                }
                return game;
            }
        }

        public static Game GetGame(int id)
        {
            using (var db = new ChessContext())
            {
                return db.Games.Find(id);
            }
        }

        public static Game MakeMove(int id, string move)
        {
            var game = GetGame(id);
            if(game == null || game.Status != "play")
            {
                return game;
            }

            Chess next = new Chess(game.FEN);
            next.Move(move);

            if(next.Fen == game.FEN)
            {
                return game;
            }

            using(var db = new ChessContext())
            {
                db.Games.Attach(game);
                game.FEN = next.Fen;
                db.Entry(game).Property(x => x.FEN).IsModified = true;
                if (next.IsCheck())
                {
                    game.FEN = "done";
                    db.Entry(game).Property(x => x.Status).IsModified = true;
                }
                db.SaveChanges();
            }
            return game;
        }

        private static Game CreateNewGame()
        {
            using (var db = new ChessContext())
            {
                Game game = new Game()
                {
                    FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
                    Status = "play"
                };

                db.Games.Add(game);
                db.SaveChanges();

                return game;
            }
        }
    }
}