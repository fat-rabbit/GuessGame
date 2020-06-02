using System;
using GuessGame.Players.MemoriesTactics;

namespace GuessGame.Players
{
    public enum PlayerType
    {
        /// <summary>
        /// Guessing random
        /// </summary>
        Random,

        /// <summary>
        /// Guessing random, but remember attempts made by themselves
        /// </summary>
        Memory,

        /// <summary>
        /// Guessing following a sequence 
        /// </summary>
        Thorough,

        /// <summary>
        /// Guessing random, but remember attempts made by other players
        /// </summary>
        Cheater,

        /// <summary>
        /// Guessing following a sequence, but remember attempts made by all players
        /// </summary>
        ThoroughCheater
        
    }

    public static class PlayerFactory
    {
        public static Player GetPlayer(PlayerType type, string name)
        {
            switch (type)
            {
                case PlayerType.Random:
                {
                    return new RandomPlayer(name);
                }
                case PlayerType.Memory:
                {
                    var player = new RandomPlayer(name);
                    return new MemoryOwn(player);
                }
                case PlayerType.Thorough:
                    return new ThoroughPlayer(name);
                case PlayerType.Cheater:
                {
                    var player = new RandomPlayer(name);
                    return new MemoryOthers(player);
                }
                case PlayerType.ThoroughCheater:
                {
                    var player = new ThoroughPlayer(name);   
                    
                    // as far as this one is thinking in sequence, their attempts won't repeat themselves anyway;
                    // so no needs to check on their own guesses;
                    
                    // var memoryOwn = new MemoryOwn(player);
                   return new MemoryOthers(player);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}