using System.Collections.Generic;
using System.Linq;
using GuessGame.Game;
using GuessGame.Players;
using GuessGame.Players.MemoriesTactics;
using Moq;
using NUnit.Framework;

namespace GuessGame.Test.Game
{
    public class GameFinishesOk
    {
        private Board _board;
        private Queue<int> _seed;

        [SetUp]
        public void Setup()
        {
            _seed = new Queue<int>(Enumerable.Range(Globals.WeightRange.from,
                    Globals.WeightRange.to - Globals.WeightRange.from + 1).Select(x => x)
            );
            string[] testNames = {"Huey", "Dewey", "Louie"};
            IList<IPlayer> playersCollection = new List<IPlayer>();

            foreach (var testName in testNames)
            {
                var playerMock = new Mock<IPlayer>();
                playerMock.Setup(x => x.Name).Returns(testName);

                playerMock.Setup(x => x.Guess()).Returns(_seed.Dequeue);
                var player = new MemoryOthers(playerMock.Object);

                playersCollection.Add(player);
            }

            _board = new Board(playersCollection);
        }

        [TearDown]
        public void Delete()
        {
            _board.Clean();
        }

        [Test]
        public void Test1()
        {
            _board.Play();
            var totalAttempts = _board.Players.Sum(x => x.AttemptsAmount);

            Assert.LessOrEqual(totalAttempts, Globals.AttemptAmount);
            Assert.AreEqual(GameStatus.Ended, _board.GameResult.Status);
        }
    }
}