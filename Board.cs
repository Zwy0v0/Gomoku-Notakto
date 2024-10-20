namespace BoardGameAPP
{
    public class Board
    {
        private int[,] _cells;

        public Board(int rows, int cols)
        {
            _cells = new int[rows, cols];
        }

        public int GetRows() => _cells.GetLength(0);
        public int GetCols() => _cells.GetLength(1);

        public int GetCell(Move move) => _cells[move.Row - 1, move.Col - 1];
        public void SetCell(Move move, int player) => _cells[move.Row - 1, move.Col - 1] = player;

       
    }
}
