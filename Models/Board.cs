namespace ProjetJeuDames.Models
{
    public class Board
    {
        private Piece[,] pieces;

        public Board()
        {
            pieces = new Piece[8, 8];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Initialisation du plateau

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 != 0)
                    {
                        if (row < 3)
                            pieces[row, col] = new Piece(PieceType.Black);
                        else if (row > 4)
                            pieces[row, col] = new Piece(PieceType.White);
                        else
                            pieces[row, col] = new Piece(PieceType.None);
                    }
                    else
                    {
                        pieces[row, col] = new Piece(PieceType.None);
                    }
                }
            }
        }

        public Piece GetPieceAtPosition(int row, int col)
        {
            // Récupération de la pièce à une position donnée

            if (row < 0 || row >= 8 || col < 0 || col >= 8)
            {
                return null;
            }

            return pieces[row, col];
        }

        public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (IsMoveValid(fromRow, fromCol, toRow, toCol))
            {
                Piece piece = pieces[fromRow, fromCol];
                pieces[fromRow, fromCol] = new Piece(PieceType.None);
                pieces[toRow, toCol] = piece;

                // Vérifier la prise de pièce (si nécessaire)
                int diffRow = toRow - fromRow;
                int diffCol = toCol - fromCol;

                if (Math.Abs(diffRow) == 2 && Math.Abs(diffCol) == 2)
                {
                    int capturedRow = fromRow + (diffRow / 2);
                    int capturedCol = fromCol + (diffCol / 2);

                    pieces[capturedRow, capturedCol] = new Piece(PieceType.None);
                    // Réaliser toute autre logique nécessaire pour gérer la prise
                }
            }
            else
            {
                // Gérer le mouvement invalide
                // Exemple : afficher un message d'erreur, annuler le mouvement, etc.
                Console.WriteLine("Mouvement invalide. Veuillez sélectionner une case valide.");
                // Ajouter d'autres actions en cas de mouvement invalide
            }
        }


        public bool IsMoveValid(int fromRow, int fromCol, int toRow, int toCol)
        {
            // Vérifier si c'est à sont tour de jouer



            // Vérifier les limites du plateau de jeu
            if (fromRow < 0 || fromRow >= 8 || fromCol < 0 || fromCol >= 8 ||
                toRow < 0 || toRow >= 8 || toCol < 0 || toCol >= 8)
            {
                return false;
            }

            // Vérifier si la case de destination est occupée
            if (pieces[toRow, toCol].Type != PieceType.None)
            {
                return false;
            }

            // Calculer la différence de déplacement
            int diffRow = toRow - fromRow;
            int diffCol = toCol - fromCol;

            // Vérifier le mouvement diagonal
            if (Math.Abs(diffRow) != Math.Abs(diffCol))
            {
                return false;
            }

            // Vérifier la distance du déplacement (maximum de 2 cases pour une prise)
            if (Math.Abs(diffRow) > 2 || Math.Abs(diffCol) > 2)
            {
                return false;
            }

            // Ajouter ici d'autres règles spécifiques pour vérifier la validité du mouvement

            return true;
        }

    }
}
