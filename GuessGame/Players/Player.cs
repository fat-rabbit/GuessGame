using System.Diagnostics;

namespace GuessGame.Players
{
    public interface IPlayer
    {
        string Name { get; }
        int AttemptsAmount { get; }
        void SkipNextRounds(int toSkip);
        int MakesAMove();
        int Guess();
    }

    public abstract class Player : IPlayer
    {
        protected Player(string name)
        {
            Name = name;
        }

        private int RoundsToSkip { get; set; }
        public string Name { get; }

        public int AttemptsAmount { get; private set; }

        public void SkipNextRounds(int toSkip)
        {
            RoundsToSkip = toSkip;
        }

        /// <summary>
        ///     returns -1 in case if current round is skipped
        /// </summary>
        /// <returns></returns>
        public int MakesAMove()
        {
            if (RoundsToSkip > 0)
            {
                --RoundsToSkip;
                Debug.WriteLine(RoundsToSkip > 0
                    ? $"{Name} skips next {RoundsToSkip} rounds"
                    : $"{Name} skips this rounds");
                return -1;
            }

            ++AttemptsAmount;
            var guess = Guess();

            Debug.WriteLine($"{Name}: I guess it's {guess}");
            return guess;
        }

        public abstract int Guess();
    }
}