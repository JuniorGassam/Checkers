namespace ProjetJeuDames.Models
{
    public enum PieceType
    {
        None,
        Black,
        White
    }

    public class Piece
    {
        public PieceType Type { get; set; }

        public Piece(PieceType type)
        {
            Type = type;
        }
    }
}
