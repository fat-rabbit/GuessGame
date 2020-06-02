using System;

namespace GuessGame.Players
{
    public abstract class Player
    {
        public readonly string Name;
        public int RoundsToSkip;

        protected Player(string name)
        {
            Name = name;
        }

        public int AttemptsAmount { get; private set; }

        /// <summary>
        ///     returns -1 in case if current round is skipped
        /// </summary>
        /// <returns></returns>
        public int MakesAMove()
        {
            if (RoundsToSkip > 0)
            {
                --RoundsToSkip;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(RoundsToSkip > 0
                    ? $"{Name} skips next {RoundsToSkip} rounds"
                    : $"{Name} skips this rounds");
                return -1;
            }

            ++AttemptsAmount;
            var guess = Guess();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Name}: I guess it's {guess}");
            return guess;
        }

        public abstract int Guess();
    }
}