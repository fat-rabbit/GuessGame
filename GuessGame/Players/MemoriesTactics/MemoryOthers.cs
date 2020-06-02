using System;
using GuessGame.Board;

namespace GuessGame.Players.MemoriesTactics
{
    public class MemoryOthers : MemoryTactic, IObserver<MoveEvent>
    {
        public override int Guess()
        {
            var result = BasicPlayer.Guess();

            while (Attempts.Contains(result))
                result = BasicPlayer.Guess();

            return result;
        }

        public MemoryOthers(Player player) : base(player)
        {
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Done");
        }

        public void OnError(Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        public void OnNext(MoveEvent ev)
        {
            Attempts.Add(ev.Attempt);
        }
    }
}