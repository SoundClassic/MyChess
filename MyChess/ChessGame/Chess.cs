using System.Collections.Generic;
using MyChess.Entity;
using MyChess.Enums;

namespace MyChess.ChessGame
{
    public class Chess
    {
        protected Board board;
        protected Moves moves;

        private List<FigureMoving> allMoves;

        public string Fen { get; private set; }

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            Fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        public Chess(Board board)
        {
            this.board = board;
            Fen = board.Fen;
            moves = new Moves(board);
        }

        public Chess Move(string move)
        {
            if (!IsCorrect(move))
            {
                return this;
            }
            FigureMoving figureMoving = new FigureMoving(move);
            return !moves.CanMove(figureMoving) || 
                   board.IsCheckAfterMove(figureMoving) ? this : new Chess(board.Move(figureMoving));
        }

        private bool IsCorrect(string move)
        {
            return move.Length == 5 &&
                (move[0] == 'B' || move[0] == 'K' ||
                 move[0] == 'b' || move[0] == 'N' ||
                 move[0] == 'k' || move[0] == 'n' ||
                 move[0] >= 'P' && move[0] <= 'R' ||
                 move[0] >= 'p' && move[0] <= 'r') && 
                 move[1] >= 'a' && move[1] <= 'h' && 
                 move[2] >= '1' && move[2] <= '8' && 
                 move[3] >= 'a' && move[3] <= 'h' && 
                 move[4] >= '1' && move[4] <= '8';
        }

        private void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (FigureOnSquare figure in board)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Square square = new Square(x, y);
                        FigureMoving figureMoving = new FigureMoving(figure, square);
                        if (moves.CanMove(figureMoving) && !board.IsCheckAfterMove(figureMoving))
                        {
                            allMoves.Add(figureMoving);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> allMoves = new List<string>();
            foreach (FigureMoving allMove in this.allMoves)
            {
                allMoves.Add(allMove.ToString());
            }
            return allMoves;
        }

        public char GetFigureAt(int x, int y)
        {
            Figure figureAt = board.GetFigureAt(new Square(x, y));
            return figureAt == Figure.None ? '.' : (char)figureAt;
        }

        public bool IsCheck() => board.IsCheck();
    }
}
