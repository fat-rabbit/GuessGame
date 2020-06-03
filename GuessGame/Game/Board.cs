using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuessGame.Players;

namespace GuessGame.Game
{
    public interface IBoard
    {
        IList<IPlayer> Players { get; }
        int Weight { get; }
        GameResult GameResult { get; }
        void Play();
        Task PlayAsync();
    }


    public class Board : IBoard
    {
        private readonly List<IObserver<MoveEvent>> _observers;

        private int _attemptsCount;
        private (int guess, IPlayer player) _closestAttempt;

        private Board()
        {
            GameResult = new GameResult();

            Players = new List<IPlayer>();
            _observers = new List<IObserver<MoveEvent>>();
        }

        public Board(IEnumerable<(string, PlayerType)> players) : this()
        {
            foreach (var (playerName, playerType) in players)
            {
                var player = PlayerFactory.GetPlayer(playerType, playerName);
                if (player is IObserver<MoveEvent> smartPlayer) _observers.Add(smartPlayer);

                Players.Add(player);
            }
        }

        public Board(IList<IPlayer> players) : this()
        {
            Players = players;

            foreach (var player in players)
                if (player is IObserver<MoveEvent> smartPlayer)
                    _observers.Add(smartPlayer);
        }

        public IList<IPlayer> Players { get; }
        public int Weight { get; private set; }
        public GameResult GameResult { get; private set; }

        public Task PlayAsync()
        {
            return Task.Run(Play);
        }

        public void Play()
        {
            SetupNewGame();
            Console.WriteLine($"Basket with fruits weights {Weight} kilos");

            var proceedGame = true;
            while (proceedGame)
                foreach (var player in Players)
                {
                    if (_attemptsCount <= 0)
                    {
                        GameResult = new GameReachedLimitResult(_closestAttempt.player, _closestAttempt.guess);
                        proceedGame = false;
                        break;
                    }

                    var guess = player.MakesAMove();

                    if (guess < 0)
                        continue;

                    --_attemptsCount;
                    if (guess != Weight)
                    {
                        ProcessAttempt(guess, player);
                    }
                    else
                    {
                        GameResult = new GameFinishedOkResult(player);
                        proceedGame = false;
                        break;
                    }
                }
        }

        private void ProcessAttempt(int guess, IPlayer player)
        {
            var precision = Math.Abs(Weight - guess);
            player.SkipNextRounds(precision / 10 - 1);

            if (precision < Math.Abs(Weight - _closestAttempt.guess))
                _closestAttempt = (guess, player);

            ShareAttempt(guess);
        }

        private void ShareAttempt(int attempt)
        {
            foreach (var observer in _observers) observer.OnNext(new MoveEvent(attempt));
        }

        public void Clean()
        {
            Players.Clear();
            _observers.Clear();
            GameResult = null;
        }

        #region Setup Game

        private void SetupNewGame()
        {
            GameResult.Reset();
            _attemptsCount = Globals.AttemptAmount;

            var random = new Random();
            Weight = random.Next(Globals.WeightRange.from, Globals.WeightRange.to);

            _closestAttempt = (Globals.WeightRange.to << 1, null);

            // The order in which players make their guesses is calculated randomly in each game.
            Shuffle();
        }

        private void Shuffle()
        {
            var random = new Random();
            for (var i = Players.Count - 1; i > 0; --i)
            {
                var j = random.Next(0, i);
                var temp = Players[i];
                Players[i] = Players[j];
                Players[j] = temp;
            }
        }

        #endregion
    }
}