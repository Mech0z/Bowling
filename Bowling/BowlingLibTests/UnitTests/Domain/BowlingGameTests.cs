using BowlingLib.Application;
using BowlingLib.Domain;
using FluentAssertions;

namespace BowlingLibTests.UnitTests.Domain
{
    public class Tests
    {
        [Test]
        public void PrintScore_WhenCalledWithEmptyGame_ShouldReturnNoPlayersAdded()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();

            // Act
            var scoreLines = bowlingGame.GetScoreLines();

            // Assert
            scoreLines.First().Should().Be("No players added to the game.");
        }

        [Test]
        public void PrintScore_When1PlayerIsAdded_ShouldEmptyScoreBoard()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();
            AddXNumberOfPlayersToGame(bowlingGame, 1);

            // Act
            var result = bowlingGame.GetScoreLines();

            // Assert
            result.First().Should().Be("Bob1: 0");
        }

        [Test]
        public void AddPlayer_WhenPlayer9IsAdded_ShouldThrow()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();
            AddXNumberOfPlayersToGame(bowlingGame, 8);
            var player9 = Player.Create("I am failure");

            // Act
            Action act = () => bowlingGame.AddPlayer(player9);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.MaxPlayersAddedRuleText);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddPlayer_WhenPlayerNameIsEmpty_ShouldThrow(string? name)
        {
            // Arrange

            // Act
            Action act = () => Player.Create(name);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.EmptyPlayerNameNotAllowedRuleText);
        }

        [Test]
        public void AddPlayer_WhenPlayerNameTooLong_ShouldThrow()
        {
            // Arrange

            // Act
            Action act = () => Player.Create("012345678901234567890");

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.MaxPlayerNameLengthRuleText);
        }

        private void AddXNumberOfPlayersToGame(BowlingGame bowlingGame, int numberOfTimesToAdd)
        {
            for (var i = 0; i < numberOfTimesToAdd; i++)
            {
                var player1 = Player.Create($"Bob{i+1}");
                bowlingGame.AddPlayer(player1);
            }
        }
    }
}