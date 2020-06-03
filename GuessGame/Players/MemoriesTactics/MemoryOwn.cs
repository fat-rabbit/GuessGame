namespace GuessGame.Players.MemoriesTactics
{
    public class MemoryOwn : MemoryTactic
    {
        public MemoryOwn(IPlayer player) : base(player)
        {
        }

        public override int Guess()
        {
            var result = base.Guess();

            Attempts.Add(result);
            return result;
        }
    }
}