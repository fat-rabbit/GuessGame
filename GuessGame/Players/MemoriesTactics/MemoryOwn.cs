namespace GuessGame.Players.MemoriesTactics
{
    public class MemoryOwn : MemoryTactic
    {
        public override int Guess()
        {
            var result = BasicPlayer.Guess();
            
            while (Attempts.Contains(result))
                result = BasicPlayer.Guess();
            
            Attempts.Add(result);
            return result;
        }

        public MemoryOwn(Player player) : base(player)
        {
        }
    }
}