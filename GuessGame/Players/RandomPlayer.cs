using System;

namespace GuessGame.Players
{
    public class RandomPlayer: Player
    {
        readonly Random _randomGen = new Random();
        
        public override int Guess()
        {
            return _randomGen.Next(Globals.WeightRange.from, Globals.WeightRange.to);
        }

        public RandomPlayer(string name) : base(name)
        {
        }
    }
}