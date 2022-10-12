using System.Collections;
using System.Collections.Generic;
using System.Text;
using MyChess.Enums;
using MyChess.Entity;

namespace MyChess.ChessGame
{
    public class Board : IEnumerable<FigureOnSquare>
    {
        protected Figure[,] figures;

        public string Fen { get; private set; }
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            string[] splitFen = Fen.Split();
            if (splitFen.Length != 6)
            {
                return;
            }

            Fen = fen;
            MoveColor = splitFen[1] == "b" ? Color.Black : Color.White;
            MoveNumber = int.Parse(splitFen[5]);

            figures = new Figure[8, 8];
            string figureInfo = splitFen[0];
            for (int i = 8; i >= 2; --i)
            {
                figureInfo = figureInfo.Replace(i.ToString(), (i - 1).ToString() + "1");
            }
            figureInfo = figureInfo.Replace("1", ".");

            string[] splitInfo = figureInfo.Split('/');
            for (int i = 7; i >= 0; --i)
            {
                for (int j = 0; j < 8; j++)
                {
                    figures[j, i] = splitInfo[7 - i][j] == '.' ? Figure.None : (Figure)splitInfo[7 - i][j];
                }
            }
        }

        public Board Move(FigureMoving figureMoving)
        {
            Board board = new Board(Fen);

            board.SetFigureAt(figureMoving.From, Figure.None);
            board.SetFigureAt(figureMoving.To, 
                figureMoving.Promotion == Figure.None ? figureMoving.Figure : figureMoving.Promotion);

            if (MoveColor == Color.Black)
            {
                board.MoveNumber++;
            }
            board.MoveColor = MoveColor.FlipColor();
            board.GenerateFen();
            return board;
        }

        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.IsSquare())
            {
                figures[square.X, square.Y] = figure;
            }
        }

        public Figure GetFigureAt(Square square) => 
            square.IsSquare() ? figures[square.X, square.Y] : Figure.None;

        private void GenerateFen() =>
            Fen = $"{FenFigures()} {(MoveColor == Color.White ? "w" : "b")} - - 0 {MoveNumber}";

        private string FenFigures()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 7; i >= 0; --i)
            {
                for (int j = 0; j < 8; j++)
                {
                    stringBuilder.Append(figures[j, i] == Figure.None ? '1' : (char)figures[j, i]);
                }
                if (i > 0)
                {
                    stringBuilder.Append('/');
                }
            }
            string str = "11111111";
            for (int length = 8; length >= 2; --length)
            {
                stringBuilder.Replace(str.Substring(0, length), length.ToString());
            }
            return stringBuilder.ToString();
        }

        public bool IsCheck()
        {
            Board board = new Board(Fen)
            {
                MoveColor = MoveColor.FlipColor()
            };
            return board.CanEatKing();
        }

        public bool IsCheckAfterMove(FigureMoving figureMoving) => 
            Move(figureMoving).CanEatKing();

        private bool CanEatKing()
        {
            Square badKing = FindBadKing();
            Moves moves = new Moves(this);
            foreach (FigureOnSquare yieldFigure in this)
            {
                FigureMoving figureMoving = new FigureMoving(yieldFigure, badKing);
                if (moves.CanMove(figureMoving))
                {
                    return true;
                }
            }
            return false;
        }

        private Square FindBadKing()
        {
            Figure figure = MoveColor == Color.Black ? Figure.WhiteKing : Figure.BlackKing;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Square square = new Square(x, y);
                    if (GetFigureAt(square) == figure)
                    {
                        return square;
                    }
                }
            }
            return Square.None;
        }

        public IEnumerator<FigureOnSquare> GetEnumerator()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Square square = new Square(x, y);
                    if (GetFigureAt(square).GetColor() == MoveColor)
                    {
                        yield return new FigureOnSquare(GetFigureAt(square), square);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
