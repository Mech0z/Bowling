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
            Action act = () => bowlingGame.AddPlayerName(name!);

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

        [Test]
        public void FullGame_WhenRollingSampleGame_ShouldGive133Points()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();
            bowlingGame.AddShot(1);
            bowlingGame.AddShot(4);

            bowlingGame.AddShot(4);
            bowlingGame.AddShot(5);

            bowlingGame.AddShot(6);
            bowlingGame.AddShot(4);

            bowlingGame.AddShot(5);
            bowlingGame.AddShot(5);

            bowlingGame.AddShot(10);

            bowlingGame.AddShot(0);
            bowlingGame.AddShot(1);

            bowlingGame.AddShot(7);
            bowlingGame.AddShot(3);

            bowlingGame.AddShot(6);
            bowlingGame.AddShot(4);

            bowlingGame.AddShot(10);

            bowlingGame.AddShot(2);
            bowlingGame.AddShot(8);

            bowlingGame.AddShot(6);

            // Act
            var totalPoints = bowlingGame.GetTotalPoints();

            // Assert
            totalPoints.Should().Be(133);
        }

        [Test]
        public void FullGame_WhenRolling10Strikes_ShouldGive300Points()
        {
            // Arrange
            var bowlingGame = BowlingGame.Create();
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);
            bowlingGame.AddShot(10);

            // Act
            var totalPoints = bowlingGame.GetTotalPoints();

            // Assert
            totalPoints.Should().Be(300);
        }
    }
}