using System.Collections.Generic;

namespace GuessGame.Players.MemoriesTactics
{
    public abstract class MemoryTactic : Player
    {
        private readonly IPlayer _basicPlayer;
        protected readonly HashSet<int> Attempts;

        protected MemoryTactic(IPlayer player) : base(player.Name)
        {
            _basicPlayer = player;
            Attempts = new HashSet<int>();
        }

        public override int Guess()
        {
            var result = _basicPlayer.Guess();

            while (Attempts.Contains(result))
                result = _basicPlayer.Guess();

            return result;
        }
    }
}