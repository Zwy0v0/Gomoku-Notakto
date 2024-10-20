namespace BoardGameAPP
{
    public class GomokuGame : BaseGame
    {
        public GomokuGame() : base(15, 15) { }
        
        public static void DisplayBoard(BaseGame game)
        {
            if (game is GomokuGame gomokuGame)
            {
                Board board = gomokuGame.Board;
                Console.WriteLine("Board State：");

                for (int i = 1; i <= board.GetRows(); i++)
                {
                    for (int j = 1; j <= board.GetCols(); j++)
                    {
                        int cellValue = board.GetCell(new Move(i, j));
                        char displayChar = cellValue switch
                        {
                            1 => 'X', // Player 1's move
                            2 => 'O', // Player 2's move
                            _ => ' ',  // Empty cell
                        };
                        Console.Write($"[{displayChar}] ");
                    }
                    Console.WriteLine(); 
                }
                Console.WriteLine(); 
            }
        }
        
        public override void CheckVictory()
        {
            //Gomoku victory checking logic
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    if (Board.GetCell(new Move(row + 1, col + 1)) != 0)
                    {
                        if (CheckWin(row, col))
                        {
                            GameState = (CurrentPlayer == 1) ? GameState.Player1Win : GameState.Player2Win;
                            return;
                        }
                    }
                }
            }

            bool draw = true;
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    if (Board.GetCell(new Move(row + 1, col + 1)) == 0)
                    {
                        draw = false;
                        break;
                    }
                }
                if (!draw) break;
            }

            if (draw)
            {
                GameState = GameState.Draw;
            }
        }

        private bool CheckWin(int row, int col)
        {
            int player = Board.GetCell(new Move(row + 1, col + 1));

            // Check horizontal, vertical, and diagonal
            return CheckLine(row, col, 1, 0, player) || // Horizontal
                   CheckLine(row, col, 0, 1, player) || // Vertical
                   CheckLine(row, col, 1, 1, player) || // Diagonal \
                   CheckLine(row, col, 1, -1, player);  // Diagonal /
        }

        private bool CheckLine(int startRow, int startCol, int rowDir, int colDir, int player)
        {
            int count = 0;
            for (int i = -4; i <= 4; i++)
            {
                int row = startRow + i * rowDir;
                int col = startCol + i * colDir;
                if (row >= 0 && row < Board.GetRows() && col >= 0 && col < Board.GetCols() &&
                    Board.GetCell(new Move(row + 1, col + 1)) == player)
                {
                    count++;
                    if (count == 5)
                        return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }
    }
}
