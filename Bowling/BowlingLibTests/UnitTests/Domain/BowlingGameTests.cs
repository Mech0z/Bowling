using BowlingLib.Application;
using BowlingLib.Domain;
using FluentAssertions;

namespace BowlingLibTests.UnitTests.Domain
{
    public class Tests
    {
        [Test]
        public void PrintScore_WhenCalledWithoutName_ShouldThrow()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();

            // Act
            Action act = () => bowlingGame.GetScoreBoard();

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.NoPlayerNameAddedRuleText);
        }

        [Test]
        public void PrintScore_When1PlayerIsAdded_ShouldEmptyScoreBoard()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();
            bowlingGame.AddPlayerName("Bob");

            // Act
            var result = bowlingGame.GetScoreBoard();

            // Assert
            result.Should().Be("Bob: 0");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddPlayer_WhenPlayerNameIsEmpty_ShouldThrow(string? name)
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();

            // Act
            Action act = () => bowlingGame.AddPlayerName(name);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.EmptyPlayerNameNotAllowedRuleText);
        }

        [Test]
        public void AddPlayer_WhenPlayerNameTooLong_ShouldThrow()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();

            // Act
            Action act = () => bowlingGame.AddPlayerName("012345678901234567890");

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.MaxPlayerNameLengthRuleText);
        }
    }
}