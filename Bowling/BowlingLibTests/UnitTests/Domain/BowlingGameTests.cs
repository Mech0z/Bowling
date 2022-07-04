using BowlingLib.Domain;
using FluentAssertions;

namespace BowlingLibTests.UnitTests.Domain
{
    public class Tests
    {
        [Test]
        public void PrintScore_Should_ReturnEmpty()
        {
            // Arrange
            
            // Act
            var bowlingGame = BowlingGame.Create();

            // Assert
            bowlingGame.PrintScore().Should().Be(string.Empty);
        }
    }
}