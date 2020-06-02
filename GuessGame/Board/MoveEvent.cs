namespace GuessGame.Board
{
    public class MoveEvent
    {
        public int Attempt { get; }

        public MoveEvent(int attempt)
        {
            Attempt = attempt;
        }
    }
}

