using System;
using GuessGame.Players;

namespace GuessGame.Game
{
    public enum GameStatus
    {
        InProgress,
        ReachedLimit,
        Ended
    }

    public class GameResult
    {
        public GameResult()
        {
            Status = GameStatus.InProgress;
        }

        protected GameResult(GameStatus status)
        {
            Status = status;
        }

        public GameStatus Status { get; private set; }
        protected IPlayer Winner { get; set; }
        protected int ClosestGuess { get; set; }

        public void Reset()
        {
            Status = GameStatus.InProgress;
            Winner = default;
            ClosestGuess = default;
        }

        public override string ToString()
        {
            var result = Status switch
            {
                GameStatus.InProgress => "Game still running...",
                GameStatus.Ended => $"{Winner.Name} win! They did {Winner.AttemptsAmount} attempts!",
                GameStatus.ReachedLimit => ("Amount of guesses reached 100. " +
                                            $"The closest assumption has been made by {Winner.Name}, and it`s {ClosestGuess}"
                ),
                _ => throw new ArgumentOutOfRangeException()
            };

            return result;
        }
    }

    public class GameReachedLimitResult : GameResult
    {
        public GameReachedLimitResult(IPlayer winner, int closestGuess) : base(GameStatus.ReachedLimit)
        {
            Winner = winner;
            ClosestGuess = closestGuess;
        }
    }

    public class GameFinishedOkResult : GameResult
    {
        public GameFinishedOkResult(IPlayer player) : base(GameStatus.Ended)
        {
            Winner = player;
        }
    }
}