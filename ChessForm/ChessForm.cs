using System;
using System.Drawing;
using System.Windows.Forms;
using MyChess.ChessGame;
using ChessForm.ChessClient;
using System.Net;

namespace ChessForm
{
    public partial class ChessForm : Form
    {
        #region Переменные
        protected Chess chess;
        protected Client client;
        private const int SIZE = 60;
        private Panel[,] cells;
        private bool wait;
        private bool connect;
        int xFrom, yFrom;
        int id;
        #endregion
        //private const string URL = "https://localhost:44348/api/Games/";
        public ChessForm()
        {
            InitializeComponent();
            wait = true;
            connect = false;
            InitCells();
            ShowMarks();
        }

        #region Отображение доски и фигур
        private void InitCells()
        {
            cells = new Panel[8, 8];
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    cells[x, y] = AddCell(x, y);
        }

        private void ShowPosition()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    ShowFigure(x, y, chess.GetFigureAt(x, y));
            MarkSquares();
        }

        private void ShowMarks()
        {
            ShowMarksX(5);
            ShowMarksX(515);
            ShowMarksY(5);
            ShowMarksY(515);
        }

        private void ShowMarksX(int y)
        {
            int x = -10;
            for (int i = 0; i < 8; i++)
            {
                AddSymbol(Convert.ToChar('A' + i), x += SIZE, y);
            }
        }

        private void ShowMarksY(int x)
        {
            int y = -10;
            for (int i = 0; i < 8; i++)
            {
                AddSymbol(Convert.ToChar('8' - i), x, y += SIZE);
            }
        }

        private Label AddSymbol(char symbol, int x, int y)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(x, y);
            label.Name = "l" + x + y;
            label.Size = new Size(29, 27);
            label.Text = symbol.ToString();
            Board.Controls.Add(label);
            return label;
        }

        private void ShowFigure(int x, int y, char figure)
        {
            cells[x, y].BackgroundImage = GetFigureImage(figure);
        }

        private Image GetFigureImage(char figure)
        {
            switch (figure)
            {
                case 'R': return Properties.Resources.WhiteRook;
                case 'N': return Properties.Resources.WhiteKnight;
                case 'B': return Properties.Resources.WhiteBishop;
                case 'Q': return Properties.Resources.WhiteQueen;
                case 'K': return Properties.Resources.WhiteKing;
                case 'P': return Properties.Resources.WhitePawn;

                case 'r': return Properties.Resources.BlackRook;
                case 'n': return Properties.Resources.BlackKnight;
                case 'b': return Properties.Resources.BlackBishop;
                case 'q': return Properties.Resources.BlackQueen;
                case 'k': return Properties.Resources.BlackKing;
                case 'p': return Properties.Resources.BlackPawn;
                default: return null;
            }
        }

        private Panel AddCell(int x, int y)
        {
            Panel panel = new Panel();
            panel.BackColor = GetColor(x, y);
            panel.Location = GetLocation(x, y);
            panel.Name = "p" + x + y;
            panel.Size = new Size(SIZE, SIZE);
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.MouseClick += new MouseEventHandler(Board_MouseClick);
            Board.Controls.Add(panel);
            return panel;
        }

        private System.Drawing.Color GetColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? System.Drawing.Color.DarkGray : System.Drawing.Color.White;
        }

        private Point GetLocation(int x, int y)
        {
            return new Point(x * SIZE + SIZE / 2,
                       (7 - y) * SIZE + SIZE / 2);
        }
        #endregion

        #region Отображение доступных ходов
        private void MarkSquares()
        {
            ResetColor();
            if (wait) MarkSquaresFrom();
            else MarkSquaresTo();
        }

        private void MarkSquaresFrom()
        {
            foreach (string move in chess.GetAllMoves())
            {
                int x = move[1] - 'a';
                int y = move[2] - '1';
                cells[x, y].BackColor = GetMarkedColor(x, y);
            }
        }

        private System.Drawing.Color GetMarkedColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Green;
        }

        private void MarkSquaresTo()
        {
            string suf = chess.GetFigureAt(xFrom, yFrom) + ToCoordinate(xFrom, yFrom);
            int x, y;
            foreach (string move in chess.GetAllMoves())
            {
                if (move.StartsWith(suf))
                {
                    x = move[3] - 'a';
                    y = move[4] - '1';
                    cells[x, y].BackColor = GetMarkedColor(x, y);
                }
            }
        }

        private string ToCoordinate(int x, int y)
        {
            return ((char)('a' + x)).ToString() + ((char)('1' + y)).ToString();
        }

        private void ResetColor()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    cells[x, y].BackColor = GetColor(x, y);
        }
        #endregion

        private void Connect_Click(object sender, EventArgs e)
        {
            if (URL.Text == null || URL.Text == "")
            {
                MessageBox.Show("Введите URL!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string url = URL.Text.Substring(0, URL.Text.Length - 2);
                id = int.Parse(URL.Text.Substring(URL.Text.Length - 2).Replace("/", ""));
                client = new Client(url);
                chess = new Chess(client.GetCurrentGame(id).Fen);
                connect = true;
                ShowPosition();
            }
            catch (FormatException)
            {
                MessageBox.Show("Не удалось найти партию!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (WebException)
            {
                MessageBox.Show("Не удалось подключится к партии!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Connect.Enabled = false;
        }

        private void WindowsChess_MouseClick(object sender, MouseEventArgs e)
        {
            if (!connect) return;
            chess = new Chess(client.GetCurrentGame(id).Fen);
            ShowPosition();
        }

        private void Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (!connect) return;
            string xy = ((Panel)sender).Name.Substring(1);
            int x = xy[0] - '0';
            int y = xy[1] - '0';

            if (wait)
            {
                wait = false;
                xFrom = x;
                yFrom = y;
            }
            else
            {
                wait = true;
                string figure = chess.GetFigureAt(xFrom, yFrom).ToString();
                string move = figure + ToCoordinate(xFrom, yFrom) + ToCoordinate(x, y);
                chess = chess.Move(client.SendMove(move).Fen);
            }
            chess = new Chess(client.GetCurrentGame(id).Fen);
            ShowPosition();
        }
    }
}
