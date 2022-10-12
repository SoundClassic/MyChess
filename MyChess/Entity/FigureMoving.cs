using System;
using MyChess.Enums;

namespace MyChess.Entity
{
    public class FigureMoving
    {
        public Square From { get; private set; }
        public Square To { get; private set; }
        public Figure Figure { get; private set; }
        public Figure Promotion { get; private set; }

        public FigureMoving(FigureOnSquare figureOnSquare, Square to, Figure promotion = Figure.None)
        {
            Figure = figureOnSquare.Figure;
            From = figureOnSquare.Square;
            To = to;
            Promotion = promotion;
        }

        public FigureMoving(string move)
        {
            Figure = (Figure)move[0];
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = move.Length == 6 ? (Figure)move[5] : Figure.None;
        }

        public int DeltaX => To.X - From.X;
        public int DeltaY => To.Y - From.Y;


        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);


        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);

        public override string ToString() => 
            $"{(char)Figure}{From.Name}{To.Name}" +
            $"{((Promotion == Figure.None) ? string.Empty : ((char)Promotion).ToString())}";
    }
}
