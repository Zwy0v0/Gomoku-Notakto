namespace BoardGameAPP
{
    public class AIPlayer : Player
    {
        public AIPlayer() : base(2)
        {
        }
        //The AI logic consists of randomly generating a piece around an existing piece on the board.
        public override Move GetMove(BaseGame game)
        {
            // Get the number of rows and columns of the board
            int rows = game.Board.GetRows();
            int cols = game.Board.GetCols();
            Random random = new Random();

            // Auxiliary function that checks all the neighbors of a given position
            bool IsAdjacentToOccupiedCell(int row, int col)
            {
                int[] dRows = { -1, 0, 1, 0, -1, 1, 1, -1 };
                int[] dCols = { 0, 1, 0, -1, -1, 1, -1, 1 };

                for (int k = 0; k < dRows.Length; k++)
                {
                    int newRow = row + dRows[k];
                    int newCol = col + dCols[k];

                    if (newRow >= 1 && newRow <= rows && newCol >= 1 && newCol <= cols &&
                        game.Board.GetCell(new Move(newRow, newCol)) != 0)
                    {
                        return true; // Position adjacent to occupied grid
                    }
                }
                return false;
            }

            // Store all empty locations that fulfill the condition
            var adjacentEmptyMoves = new List<Move>();

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    Move move = new Move(i, j);

                    if (move.IsWithinBounds(game.Board) && game.Board.GetCell(move) == 0)
                    {
                        if (IsAdjacentToOccupiedCell(i, j))
                        {
                            adjacentEmptyMoves.Add(move); // Add to the list of neighboring occupied locations
                        }
                    }
                }
            }

            // If a location is found that satisfies the condition, a randomly selected
            if (adjacentEmptyMoves.Count > 0)
            {
                int randomIndex = random.Next(adjacentEmptyMoves.Count);
                return adjacentEmptyMoves[randomIndex];
            }

            // If there are no spaces adjacent to an occupied position, an empty position is randomly selected
            var allEmptyMoves = new List<Move>();
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    Move move = new Move(i, j);
                    if (move.IsWithinBounds(game.Board) && game.Board.GetCell(move) == 0)
                    {
                        allEmptyMoves.Add(move);
                    }
                }
            }

            if (allEmptyMoves.Count > 0)
            {
                int randomIndex = random.Next(allEmptyMoves.Count);
                return allEmptyMoves[randomIndex];
            }

            throw new InvalidOperationException("No effective moving position!");
        }
    }
}