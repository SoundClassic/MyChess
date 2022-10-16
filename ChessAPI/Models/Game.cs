using System.ComponentModel.DataAnnotations;

namespace ChessAPI.Models
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string FEN { get; set; }


        [StringLength(255)]
        public string Status { get; set; }

    }
}