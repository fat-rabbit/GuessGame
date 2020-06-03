using System.Collections.Generic;
using GuessGame.Game;
using GuessGame.Players;
using GuessGame.Players.MemoriesTactics;
using Moq;
using NUnit.Framework;

namespace GuessGame.Test.Players
{
    public class MemoryOtherTest
    {
        private MemoryOthers _player;
        private Queue<int> _seed;

        [SetUp]
        public void Setup()
        {
            _seed = new Queue<int>(new[] {1, 2, 3, 5, 8, 13, 21});
            var playerMock = new Mock<IPlayer>();

            playerMock.Setup(x => x.Guess()).Returns(_seed.Dequeue);
            _player = new MemoryOthers(playerMock.Object);
        }

        [Test]
        public void Test1()
        {
            int[] otherMoves = {1, 2, 5, 8, 13};
            int[] expected = {2, 3, 8, 13, 21};

            for (var i = 0; i < 5; ++i)
            {
                _player.OnNext(new MoveEvent(otherMoves[i]));
                var res = _player.MakesAMove();
                Assert.AreEqual(res, expected[i]);
            }

            Assert.Pass();
        }
    }
}