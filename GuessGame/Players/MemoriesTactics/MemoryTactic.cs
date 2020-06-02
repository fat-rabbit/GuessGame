using System.Collections.Generic;

namespace GuessGame.Players.MemoriesTactics
{
    public abstract class MemoryTactic: Player
    {
        protected readonly Player BasicPlayer;
        protected readonly HashSet<int> Attempts;
        
        protected MemoryTactic(Player player):base(player.Name)
        {
            BasicPlayer = player;
            Attempts = new HashSet<int>();
        }
    }
}