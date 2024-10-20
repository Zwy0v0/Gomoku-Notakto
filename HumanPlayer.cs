namespace BoardGameAPP
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(int playerId) : base(playerId)
        {
        }

        public override Move GetMove(BaseGame game)
        {
            Console.WriteLine($"Please enter rows and columns (format: x y):");
            while (true)
            {
                string[] input = Console.ReadLine().Split();
                if (input.Length != 2 || !int.TryParse(input[0], out int x) || !int.TryParse(input[1], out int y))
                {
                    Console.WriteLine("Invalid input! Please enter valid rows and columns (format: x y).");
                    continue;
                }

                Move move = new Move(x, y);
                if (move.IsWithinBounds(game.Board) && game.Board.GetCell(move) == 0)
                {
                    return move;
                }
                else
                {
                    Console.WriteLine("Invalid move! Please make sure that the entered piece position is on an empty square within the board.");
                }
            }
        }
    }
}
