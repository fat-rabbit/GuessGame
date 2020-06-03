using System.Collections.Generic;
using GuessGame.Players;
using GuessGame.Players.MemoriesTactics;
using Moq;
using NUnit.Framework;

namespace GuessGame.Test.Players
{
    public class MemoryOwnTest
    {
        private Player _player;
        private Queue<int> _seed;

        [SetUp]
        public void Setup()
        {
            _seed = new Queue<int>(new[] {1, 2, 3, 1, 2, 3, 4, 1, 1, 2, 3, 4, 5});
            var playerMock = new Mock<IPlayer>();

            playerMock.Setup(x => x.Guess()).Returns(_seed.Dequeue);
            _player = new MemoryOwn(playerMock.Object);
        }

        [Test]
        public void Test1()
        {
            int[] expected = {1, 2, 3, 4, 5};

            for (var i = 0; i < 5; ++i)
            {
                var res = _player.MakesAMove();
                Assert.AreEqual(res, expected[i]);
            }
        }
    }
}