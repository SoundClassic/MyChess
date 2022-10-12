using MyChess.Enums;

namespace MyChess
{
    /// <summary>
    /// Класс помощник для методов расширения
    /// </summary>
    public static class Helper
    {
        public static Color FlipColor(this Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            if(color == Color.Black)
            {
                return Color.White;
            }
            return Color.None;
        }

        public static Color GetColor(this Figure figure)
        {
            if(figure == Figure.None)
            {
                return Color.None;
            }
            return (figure.GetHashCode() < 'a') ? Color.White : Color.Black;
        }

    }
}
