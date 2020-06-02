using System;
using System.Collections.Generic;
using GuessGame.Players;

namespace GuessGame.Board
{
    public class Board
    {
        private int _weight;
        private int _attemptsCount = 100;
        private (int guess, string playerName) _closestAttempt;
        private readonly IList<Player> _players;
        private readonly List<IObserver<MoveEvent>> _observers;

        public Board(IList<(string, PlayerType)> players)
        {
            _players = new List<Player>();
            _observers = new List<IObserver<MoveEvent>>();

            foreach (var (playerName, playerType) in players)
            {
                var player = PlayerFactory.GetPlayer(playerType, playerName);
                if (player is IObserver<MoveEvent> smartPlayer)
                {
                    _observers.Add(smartPlayer);
                }

                _players.Add(player);
            }
        }

        public void Play()
        {
            SetupNewGame();
            Console.WriteLine($"Basket with fruits weights {_weight} kilos");

            while (true)
            {
                foreach (var player in _players)
                {
                    var guess = player.MakesAMove();

                    if (guess < 0)
                        continue;
                    if (guess != _weight)
                    {
                        ProcessAttempt(guess, player);
                    }
                    else
                    {
                        Win(player);
                        return;
                    }

                    if (_attemptsCount-- == 0)
                    {
                        GameTookTooLong();
                        return;
                    }
                }
            }
        }

        private void ProcessAttempt(int guess, Player player)
        {
            var dif = Math.Abs(_weight - guess);
            player.RoundsToSkip = (dif / 10) - 1;
            if (dif < Math.Abs(_weight - _closestAttempt.guess))
            {
                _closestAttempt = (guess, player.Name);
            }

            ShareAttempt(guess);
        }

        private void ShareAttempt(int attempt)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(new MoveEvent(attempt));
            }
        }


        #region Setup Game

        private void SetupNewGame()
        {
            _attemptsCount = 100;
            var random = new Random();
            _weight = random.Next(Globals.WeightRange.from, Globals.WeightRange.to);
            _closestAttempt = (Globals.WeightRange.to, string.Empty);

            // The order in which players make their guesses is calculated randomly in each game.
            Shuffle();
        }

        private void Shuffle()
        {
            var random = new Random();
            for (var i = _players.Count - 1; i > 0; --i)
            {
                var j = random.Next(0, i);
                var temp = _players[i];
                _players[i] = _players[j];
                _players[j] = temp;
            }
        }

        #endregion

        #region Game Over

        private static void Win(Player player)
        {
            Console.Write($"{player.Name} win! They did {player.AttemptsAmount} attempts!");
        }

        private void GameTookTooLong()
        {
            Console.Write($"Amount of guesses reached 100. " +
                          $"The closest assumption, that has been made by {_closestAttempt.playerName}, is {_closestAttempt.guess}");
        }

        #endregion

        public void Clean()
        {
            _players.Clear();
            _observers.Clear();
        }
    }
}