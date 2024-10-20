namespace BoardGameAPP
{
    public abstract class Player
    {
        public int PlayerId { get; private set; }

        protected Player(int playerId)
        {
            PlayerId = playerId;
        }

        public abstract Move GetMove(BaseGame game);
    }
}
