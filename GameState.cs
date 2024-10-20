namespace BoardGameAPP
{
    //For GameSaver
    [Serializable]
    public class GameStateData
    {
        public int CurrentPlayer { get; set; }
        public int[] BoardCells { get; set; } 
        public int? CurrentBoardIndex { get; set; } //Notakto
        public string GameType { get; set; }
    }

    //For GameState

    public enum GameState
    {
        Ongoing,
        Player1Win,
        Player2Win,
        Draw
    }
}
