namespace GuessGame.Game
{
    public class MoveEvent
    {
        public MoveEvent(int attempt)
        {
            Attempt = attempt;
        }

        public int Attempt { get; }
    }
}