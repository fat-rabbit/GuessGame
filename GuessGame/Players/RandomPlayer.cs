using System;

namespace GuessGame.Players
{
    public class RandomPlayer : Player
    {
        private readonly Random _randomGen = new Random();

        public RandomPlayer(string name) : base(name)
        {
        }

        public override int Guess()
        {
            return _randomGen.Next(Globals.WeightRange.from, Globals.WeightRange.to);
        }
    }
}