using System.Collections.Generic;
using System.Linq;
using GuessGame.Game;
using GuessGame.Players;
using Moq;
using NUnit.Framework;

namespace GuessGame.Test.Game
{
    public class BoardReachedLimitTest
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            string[] testNames = {"Huey", "Dewey", "Louie"};
            IList<IPlayer> playersCollection = new List<IPlayer>();

            var i = 0;
            foreach (var testName in testNames)
            {
                var player = new Mock<IPlayer>();
                player.Setup(x => x.Name).Returns(testName);
                player.Setup(x => x.MakesAMove()).Returns(i++);

                playersCollection.Add(player.Object);
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

            Assert.Greater(totalAttempts, Globals.AttemptAmount);
            Assert.AreEqual(GameStatus.ReachedLimit, _board.GameResult.Status);
        }
    }
}