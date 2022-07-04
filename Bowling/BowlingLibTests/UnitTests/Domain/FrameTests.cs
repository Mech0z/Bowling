using BowlingLib.Application;
using BowlingLib.Domain;
using FluentAssertions;

namespace BowlingLibTests.UnitTests.Domain
{
    public class FrameTests
    {
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(2, 8, 10)]
        [TestCase(0, 10, 10)]
        public void Frame_WhenShooting_ShouldGet(int throw1, int throw2, int expectedPoints)
        {
            // Arrange
            var frame = Frame.Create();

            // Act
            frame.AddShot(throw1);
            frame.AddShot(throw2);
            
            // Assert
            frame.Points.Should().Be(expectedPoints);

        }

        [TestCase(2, 9, false, "11 points total is not allowed")]
        [TestCase(11, 1, false, "11 points in one shot is not possible")]
        public void Frame_WhenShooting_ShouldThrowInvalidNumberOfPinsAreKnockedOver(int throw1, int throw2, bool expected, string reasonText)
        {
            // Arrange
            var frame = Frame.Create();

            // Act
            Action act = () =>
            {
                frame.AddShot(throw1);
                frame.AddShot(throw2);
            };

            // Assert

            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.ImpossibleNumberOfPinsKnockedOverRuleText);
        }

        [Test]
        public void Frame_WhenHittingAStrike_FrameFinishedShouldBeTrue()
        {
            // Arrange
            var frame = Frame.Create();

            // Act
            frame.AddShot(10);

            // Assert
            frame.FrameFinished.Should().BeTrue();
        }

        [Test]
        public void Frame_WhenFrameIsFinished_ShouldThrowWhenAddingAnotherShot()
        {
            // Arrange
            var frame = Frame.Create();
            frame.AddShot(10);

            // Act
            Action act = () => frame.AddShot(1);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.FrameIsFinishedRuleText);
        }
    }
}
