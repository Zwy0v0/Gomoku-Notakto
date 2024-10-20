namespace BoardGameAPP
{
    public static class HelpSystem
    {
        public static void ShowHelp(string gameType)
        {
            if (gameType == "Gomoku")
            {
                Console.WriteLine("Gomoku Game Rules：");
                Console.WriteLine("1. The game is played on a 15x15 board.");
                Console.WriteLine("2. Players take turns placing 'X' and 'O' on the board, and the first player to form five consecutive identical pieces wins.");
                Console.WriteLine("3. If the board is filled and no player forms a pentomino, the game is a draw.");
            }
            else if (gameType == "Notakto")
            {
                Console.WriteLine("Notakto Game Rules：");
                Console.WriteLine("1. The game is played on three 3x3 boards.");
                Console.WriteLine("2. Players take turns placing 'X' and 'O' from the first board.");
                Console.WriteLine("3. The board will be locked if there are three identical pieces in a line or if the board is full.");
                Console.WriteLine("4. The game automatically switches to the next board.");
                Console.WriteLine("5. If the third board appears to have three pieces in a row, the player who placed this piece loses.");
                Console.WriteLine("6. If the third board is filled and there are no three pieces in a row, the game is drawn.");
            }
            else
            {
                Console.WriteLine("No information on this game");
            }
        }
    }
}

