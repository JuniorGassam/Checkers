using ProjetJeuDames.Models;
using ProjetJeuDames.Players;

namespace ProjetJeuDames.GameLogic
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public Game()
        {
            board = new Board();
            string[] names;
            names = GetNames();
            player1 = new Player(PieceType.Black, names[0]);
            player2 = new Player(PieceType.White, names[1]);
            currentPlayer = player1;



            using (var context = new MyDbContext())
            {
                // Création d'un utilisateur
                var newPlayer1 = new Player(currentPlayer.Type, player1.Name);
                var newPlayer2 = new Player(currentPlayer.Type, player2.Name);
                context.Players.Add(newPlayer1);
                context.Players.Add(newPlayer2);
                context.SaveChanges();
            }
            
        }

        public String[] GetNames()
        {
            Console.WriteLine("Player1's name:");
            string name1 = Console.ReadLine();

            Console.WriteLine("Player2's name:");
            string name2 = Console.ReadLine();

            string[] names = { name1, name2 };
            return names;
        }

        public void Play()
        {
            bool gameFinished = false;

            while (gameFinished == false)
            {
                DrawBoard();
                MakeMove();
                gameFinished = CheckGameFinished();
            }

        }

        public void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece currentPiece = board.GetPieceAtPosition(row, col);

                    if ((row + col) % 2 != 0)
                    {
                        if (currentPiece.Type == PieceType.None)
                        {
                            Console.Write(" - "); // Case vide
                        }
                        else if (currentPiece.Type == PieceType.Black)
                        {
                            Console.Write(" B "); // Pion noir
                        }
                        else if (currentPiece.Type == PieceType.White)
                        {
                            Console.Write(" W "); // Pion blanc
                        }
                    }
                    else
                    {
                        Console.Write("   "); // Case non jouable
                    }
                }
                Console.WriteLine(); // Passage à la ligne pour la prochaine rangée
            }
        }


        public void MakeMove()
        {
            if (currentPlayer == player1)
            {
                Console.WriteLine("\n" + currentPlayer.Name + "'s turn:");
                Console.Write("Enter the row of the piece you want to move: \n");
                int fromRow = int.Parse(Console.ReadLine());

                Console.Write("Enter the column of the piece you want to move: \n");
                int fromCol = int.Parse(Console.ReadLine());

                Console.Write("Enter the row where you want to move: \n");
                int toRow = int.Parse(Console.ReadLine());

                Console.Write("Enter the column where you want to move: \n");
                int toCol = int.Parse(Console.ReadLine());


                Piece currentPiece = board.GetPieceAtPosition(fromRow, fromCol);
                if (currentPlayer.Type == currentPiece.Type)
                {

                    if (board.IsMoveValid(fromRow, fromCol, toRow, toCol))
                    {
                        board.MovePiece(fromRow, fromCol, toRow, toCol);
                        Console.WriteLine("Piece moved successfully!");
                        // Autres actions après un mouvement réussi
                        currentPlayer = player2;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Please select a valid move.");
                        // Autres actions en cas de mouvement invalide
                    }
                }
                else
                {
                    Console.WriteLine("You can't move " + player2.Name + "'s pieces");
                }
            }
            else
            {
                Console.WriteLine("\n" + currentPlayer.Name + "'s turn:");
                Console.Write("Enter the row of the piece you want to move: \n");
                int fromRow = int.Parse(Console.ReadLine());

                Console.Write("Enter the column of the piece you want to move: \n");
                int fromCol = int.Parse(Console.ReadLine());

                Console.Write("Enter the row where you want to move: \n");
                int toRow = int.Parse(Console.ReadLine());

                Console.Write("Enter the column where you want to move: \n");
                int toCol = int.Parse(Console.ReadLine());

                Piece currentPiece = board.GetPieceAtPosition(fromRow, fromCol);
                if (currentPlayer.Type == currentPiece.Type)
                {

                    if (board.IsMoveValid(fromRow, fromCol, toRow, toCol))
                    {
                        board.MovePiece(fromRow, fromCol, toRow, toCol);
                        Console.WriteLine("Piece moved successfully!");
                        // Autres actions après un mouvement réussi
                        currentPlayer = player2;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Please select a valid move.");
                        // Autres actions en cas de mouvement invalide
                    }
                }
                else
                {
                    Console.WriteLine("You can't move " + player1.Name + "'s pieces");
                }
            }
        }


        public bool CheckGameFinished()
        {
            int blackPiecesCount = 0;
            int whitePiecesCount = 0;

            // Parcourir le plateau pour compter les pièces de chaque couleur
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece currentPiece = board.GetPieceAtPosition(row, col);
                    if (currentPiece.Type == PieceType.Black)
                    {
                        blackPiecesCount++;
                    }
                    else if (currentPiece.Type == PieceType.White)
                    {
                        whitePiecesCount++;
                    }
                }
            }

            // Vérifier si l'un des joueurs a perdu toutes ses pièces
            if (blackPiecesCount == 0 || whitePiecesCount == 0)
            {
                if (blackPiecesCount == 0)
                {
                    Console.WriteLine("Game Over! White player wins!");
                }
                else
                {
                    Console.WriteLine("Game Over! Black player wins!");
                }
                return true; // Le jeu est terminé
            }

            return false; // Le jeu n'est pas terminé
        }

    }
}
