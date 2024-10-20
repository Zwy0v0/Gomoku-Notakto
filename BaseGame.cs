namespace BoardGameAPP
{
    public abstract class BaseGame
    {
        public Board Board { get; private set; }
        protected Stack<Move> _moveHistory;
        public int CurrentPlayer { get; set; }
        public GameState GameState { get; protected set; }

        protected BaseGame(int rows, int cols)
        {
            Board = new Board(rows, cols);
            _moveHistory = new Stack<Move>();
            CurrentPlayer = 1; // Default starting player
            GameState = GameState.Ongoing;
        }

        public virtual bool MakeMove(Move move)
        {
            if (Board.GetCell(move) == 0)
            {
                Board.SetCell(move, CurrentPlayer);
                RecordMove(move);
                CheckVictory();
                CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
                return true;
            }
            return false;
        }

        public virtual void UndoMove()
        {
            if (_moveHistory.Count > 0)
            {
                Move lastMove = _moveHistory.Pop();
                Board.SetCell(lastMove, 0);
                CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
            }
        }
        public abstract void CheckVictory();

        protected void RecordMove(Move move)
        {
            _moveHistory.Push(move);
        }
      
    }
}
