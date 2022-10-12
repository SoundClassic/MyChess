using MyChess.Entity;
using MyChess.Enums;

namespace MyChess.ChessGame
{
    public class Moves
    {
        protected FigureMoving figureMoving;
        protected Board board;

        public Moves(Board board)
        { 
            this.board = board; 
        }

        public bool CanMove(FigureMoving figureMoving)
        {
            this.figureMoving = figureMoving;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        private bool CanMoveFrom() => 
            figureMoving.From.IsSquare() && figureMoving.Figure.GetColor() == board.MoveColor;

        private bool CanMoveTo() => 
            figureMoving.To.IsSquare() && 
            figureMoving.From != figureMoving.To && 
            board.GetFigureAt(figureMoving.To).GetColor() != board.MoveColor;

        private bool CanFigureMove()
        {
            switch (figureMoving.Figure)
            {
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return figureMoving.SignX != 0 && figureMoving.SignY != 0 && CanStraightMove();
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();
                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();
                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPawnMove();
                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return CanStraightMove();
                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return (figureMoving.SignX == 0 || figureMoving.SignY == 0) && CanStraightMove();
                default:
                    return false;
            }
        }

        private bool CanStraightMove()
        {
            Square square = figureMoving.From;
            do
            {
                square = new Square(square.X + figureMoving.SignX, square.Y + figureMoving.SignY);
                if (square == figureMoving.To)
                {
                    return true;
                }
            }
            while (square.IsSquare() && board.GetFigureAt(square) == Figure.None);
            return false;
        }

        private bool CanKingMove() => 
            figureMoving.AbsDeltaX <= 1 && figureMoving.AbsDeltaY <= 1;

        private bool CanKnightMove() => 
            figureMoving.AbsDeltaX == 1 && figureMoving.AbsDeltaY == 2 || 
            figureMoving.AbsDeltaX == 2 && figureMoving.AbsDeltaY == 1;

        private bool CanPawnMove()
        {
            if (figureMoving.From.Y < 1 || figureMoving.From.Y > 6)
            {
                return false;
            }
            int stepY = figureMoving.Figure.GetColor() == Color.White ? 1 : -1;
            return CanPawnGo(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY) => 
            board.GetFigureAt(figureMoving.To) != Figure.None && 
            figureMoving.AbsDeltaX == 1 && figureMoving.DeltaY == stepY;

        private bool CanPawnJump(int stepY) => 
            board.GetFigureAt(figureMoving.To) == Figure.None && 
            figureMoving.DeltaX == 0 && 
            figureMoving.DeltaY == 2 * stepY && 
            (figureMoving.From.Y == 1 || figureMoving.From.Y == 6) && 
            board.GetFigureAt(new Square(figureMoving.From.X, figureMoving.From.Y + stepY)) == Figure.None;

        private bool CanPawnGo(int stepY) => 
            board.GetFigureAt(figureMoving.To) == Figure.None && 
            figureMoving.DeltaX == 0 && figureMoving.DeltaY == stepY;
    }
}
