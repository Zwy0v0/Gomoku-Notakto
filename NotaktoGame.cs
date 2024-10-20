namespace BoardGameAPP
{   //The logic of this game is that when a board is tripled or filled, the board is cleared and the board index is updated, and the result of the game is determined on the third board at board index 2.
    //Unresolved issue: can't undo after switching boards?
    public class NotaktoGame : BaseGame
    {
        private const int NumberOfBoards = 3;
        public int _currentBoardIndex;

        public NotaktoGame() : base(3, 3)
        {
            _currentBoardIndex = 0; // Default to first board
        }

        public void DisplayBoards()
        {
            Console.Clear();
            Console.WriteLine($"Board {_currentBoardIndex + 1}:");
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    int cellValue = Board.GetCell(new Move(row + 1, col + 1, _currentBoardIndex));
                    char displayChar = cellValue switch
                    {
                        1 => 'X',
                        2 => 'O',
                        _ => ' '
                    };
                    Console.Write($"[{displayChar}] ");
                }
                Console.WriteLine();
            }
        }
        
        public override void CheckVictory()
        {
            if (_currentBoardIndex == 2)
            {
                // Check if there is a winner on the third board
                if (CheckBoardVictory(_currentBoardIndex))
                {
                    // The player who made the move that resulted in a win on the third board loses
                    GameState = (CurrentPlayer == 1) ? GameState.Player2Win : GameState.Player1Win;
                }
                else if (IsBoardFull(_currentBoardIndex))
                {
                    // If the third board is full and there is no winner, the game is a draw
                    GameState = GameState.Draw;
                }
            }
            else if (_currentBoardIndex == 0|| _currentBoardIndex == 1)
            {
                if (CheckBoardVictory(_currentBoardIndex)||IsBoardFull(_currentBoardIndex))
                {
                    SwitchBoard();
                }
                
            }
        }
        //The following methods are all for CheckVictory()
        private bool CheckBoardVictory(int boardIndex)
        {
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    if (Board.GetCell(new Move(row + 1, col + 1, boardIndex)) != 0)
                    {
                        if (CheckLine(row, col, boardIndex))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckLine(int startRow, int startCol, int boardIndex)
        {
            int player = Board.GetCell(new Move(startRow + 1, startCol + 1, boardIndex));

            return CheckDirection(startRow, startCol, boardIndex, 1, 0, player) || // Horizontal
                   CheckDirection(startRow, startCol, boardIndex, 0, 1, player) || // Vertical
                   CheckDirection(startRow, startCol, boardIndex, 1, 1, player) || // Diagonal \
                   CheckDirection(startRow, startCol, boardIndex, 1, -1, player);  // Diagonal /
        }

        private bool CheckDirection(int startRow, int startCol, int boardIndex, int rowDir, int colDir, int player)
        {
            int count = 0;
            for (int i = -2; i <= 2; i++)
            {
                int row = startRow + i * rowDir;
                int col = startCol + i * colDir;
                if (row >= 0 && row < Board.GetRows() && col >= 0 && col < Board.GetCols() &&
                    Board.GetCell(new Move(row + 1, col + 1, boardIndex)) == player)
                {
                    count++;
                    if (count == 3)
                    {
                        if (_currentBoardIndex == 2) 
                        {
                            GameState = (CurrentPlayer == 1) ? GameState.Player2Win : GameState.Player1Win;
                        }
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        private void SwitchBoard()
        {
            if (_currentBoardIndex < NumberOfBoards - 1)
            {
                Console.WriteLine($"Switch to Board {_currentBoardIndex + 1}");
                ClearBoard(_currentBoardIndex);
                _currentBoardIndex++;
                DisplayBoards(); //DisplayNewBoard
            }
        }

        private void ClearBoard(int boardIndex)
        {
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    Board.SetCell(new Move(row + 1, col + 1, boardIndex), 0); // Clear the board cells
                }
            }
        }
        private bool IsBoardFull(int boardIndex)
        {
            for (int row = 0; row < Board.GetRows(); row++)
            {
                for (int col = 0; col < Board.GetCols(); col++)
                {
                    if (Board.GetCell(new Move(row + 1, col + 1, boardIndex)) == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
    }
}
