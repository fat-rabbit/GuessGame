namespace GuessGame.Players
{
    public class ThoroughPlayer : Player
    {
        private int _lastGuess = Globals.WeightRange.from;

        public ThoroughPlayer(string name) : base(name)
        {
        }

        public override int Guess()
        {
            return _lastGuess++;
        }
    }
}