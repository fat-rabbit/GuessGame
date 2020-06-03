using System;
using GuessGame.Game;

namespace GuessGame.Players.MemoriesTactics
{
    public class MemoryOthers : MemoryTactic, IObserver<MoveEvent>
    {
        public MemoryOthers(IPlayer player) : base(player)
        {
        }

        public void OnCompleted()
        {
            Console.WriteLine("Done");
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