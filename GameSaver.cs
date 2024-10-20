using BoardGameAPP;
using System.Xml.Serialization;
//The archive needs to save the game type, the current player, and the position of the pieces on the board. Notakto needs to save the current board index.
public static class GameSaver
{
    public static void SaveGame(BaseGame game, string fileName)
    {
        GameStateData gameState = new GameStateData
        {
            CurrentPlayer = game.CurrentPlayer,
            BoardCells = ConvertBoardToArray(game.Board),
            GameType = game.GetType().Name 
        };

        if (game is NotaktoGame notaktoGame)
        {
            gameState.CurrentBoardIndex = notaktoGame._currentBoardIndex;
        }
        else
        {
            gameState.CurrentBoardIndex = null;
        }

        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameStateData));
            serializer.Serialize(fs, gameState);
        }
    }

    private static int[] ConvertBoardToArray(Board board)
    {
        int rows = board.GetRows();
        int cols = board.GetCols();
        int[] cells = new int[rows * cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                cells[row * cols + col] = board.GetCell(new Move(row + 1, col + 1, 0));
            }
        }

        return cells;
    }

    public static BaseGame LoadGame(string fileName)
    {
        GameStateData gameState;
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameStateData));
            gameState = (GameStateData)serializer.Deserialize(fs);
        }

        BaseGame game = CreateGameFromType(gameState.GameType);
        RestoreGameState(game, gameState);

        return game;
    }

    private static BaseGame CreateGameFromType(string gameType)
    {
        return gameType switch
        {
            nameof(NotaktoGame) => new NotaktoGame(),
            nameof(GomokuGame) => new GomokuGame(),
            _ => throw new InvalidOperationException("Unsupported game type！")
        };
    }

    private static void RestoreGameState(BaseGame game, GameStateData gameState)
    {
        if (game is NotaktoGame notaktoGame)
        {
            notaktoGame._currentBoardIndex = gameState.CurrentBoardIndex ?? 0;
        }

        RestoreBoardFromArray(game.Board, gameState.BoardCells);
        game.CurrentPlayer = gameState.CurrentPlayer;
    }

    private static void RestoreBoardFromArray(Board board, int[] cells)
    {
        int rows = board.GetRows();
        int cols = board.GetCols();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int index = row * cols + col;
                board.SetCell(new Move(row + 1, col + 1, 0), cells[index]);
            }
        }
    }

}
