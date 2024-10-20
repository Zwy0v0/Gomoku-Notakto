namespace BoardGameAPP
{
    public class Move
    {
        public int Row { get; }
        public int Col { get; }
        public int BoardIndex { get; } // Only used for NotaktoGame

        public Move(int row, int col, int boardIndex = 0)
        {
            Row = row;
            Col = col;
            BoardIndex = boardIndex;
        }

        public bool IsWithinBounds(Board board)
        {
            return Row > 0 && Row <= board.GetRows() && Col > 0 && Col <= board.GetCols();
        }
       
    }
}
