using System.ComponentModel.DataAnnotations;
using ProjetJeuDames.Models;

namespace ProjetJeuDames.Players
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public PieceType Type { get; }


        public Player(PieceType type, string name)
        {
            Type = type;
            Name = name;
        }

        public bool IsMyTurn(PieceType currentTurn)
        {
            return Type == currentTurn;
        }
    }
}
