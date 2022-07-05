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
        [TestCase(2, 8, null)]
        [TestCase(0, 10, null)]
        public void Points_WhenLessThan10PinsAreKnockedOver_ShouldReturnExpectedValue(int firstRoll, int secondRoll, int? expectedPoints)
        {
            // Arrange
            var frame = Frame.Create();
            var frames = GetNumberOfFrames(10);
            frame.AddAllFrames(frames);

            // Act
            frame.AddShot(firstRoll);
            frame.AddShot(secondRoll);
            
            // Assert
            frame.Points.Should().Be(expectedPoints);
        }

        [TestCase(0, 10)]
        [TestCase(1, 11)]
        [TestCase(10, 20)]
        public void Points_WhenRollingASpare_ShouldReturnExpectedValue(int firstRollForNextFrame, int? expectedPoints)
        {
            // Arrange
            var frame = Frame.Create();
            var frames = GetNumberOfFrames(10);
            frame.AddAllFrames(frames);
            frame.AddShot(8);
            frame.AddShot(2);

            // Act
            frame.NextFrame.AddShot(firstRollForNextFrame);

            // Assert
            frame.Points.Should().Be(expectedPoints);
        }

        [Test]
        public void Points_WhenRollingASpareAndNextFrameIsNotStarted_ShouldReturnNull()
        {
            // Arrange
            var frame = Frame.Create();
            var frames = GetNumberOfFrames(10);
            frame.AddAllFrames(frames);
            frame.AddShot(8);
            frame.AddShot(2);

            // Act

            // Assert
            frame.Points.Should().Be(null);
        }

        [TestCase(0,0, 10)]
        [TestCase(0, 10, 20)]
        [TestCase(1, 5, 16)]
        [TestCase(8, 2, 20)]
        public void Points_WhenRollingAStrikeAndThenNotAStrike_ShouldReturnExpectedValue(int firstRollForNextFrame, int secondRollForNextFrame, int? expectedPoints)
        {
            // Arrange
            var frame = Frame.Create();
            var frames = GetNumberOfFrames(10);
            frame.AddAllFrames(frames);
            frame.AddShot(10);

            // Act
            frame.NextFrame.AddShot(firstRollForNextFrame);
            frame.NextFrame.AddShot(secondRollForNextFrame);

            // Assert
            frame.Points.Should().Be(expectedPoints);
        }

        [TestCase(0, 0, 20)]
        [TestCase(0, 10, 20)]
        [TestCase(1, 5, 21)]
        [TestCase(8, 2, 28)]
        public void Points_WhenRolling2Strikes_ShouldReturnExpectedValue(int firstRollForNextNextFrame, int secondRollForNextNextFrame, int? expectedPoints)
        {
            // Arrange
            var frame = CreateFrameWith10Frames();
            
            frame.AddShot(10);
            frame.NextFrame.AddShot(10);
            
            // Act
            frame.NextFrame.NextFrame.AddShot(firstRollForNextNextFrame);
            frame.NextFrame.NextFrame.AddShot(secondRollForNextNextFrame);

            // Assert
            frame.Points.Should().Be(expectedPoints);
        }

        [Test]
        public void Points_WhenRolling3Strikes_ShouldReturn30Points()
        {
            // Arrange
            var frame = CreateFrameWith10Frames();

            frame.AddShot(10);
            frame.NextFrame.AddShot(10);
            frame.NextFrame.NextFrame.AddShot(10);

            // Act

            // Assert
            frame.Points.Should().Be(30);
        }

        [TestCase(2, 9, "11 points total is not allowed")]
        [TestCase(11, 1, "11 points in one shot is not possible")]
        public void Frame_WhenShooting_ShouldThrowInvalidNumberOfPinsAreKnockedOver(int firstRoll, int secondRoll, string reasonText)
        {
            // Arrange
            var frame = Frame.Create();

            // Act
            Action act = () =>
            {
                frame.AddShot(firstRoll);
                frame.AddShot(secondRoll);
            };

            // Assert

            act.Should().Throw<InvalidOperationException>()
                .WithMessage(ValidationRuleTextTemplates.ImpossibleNumberOfPinsKnockedOverRuleText, reasonText);
        }

        [Test]
        public void Frame_WhenHittingAStrike_FrameFinishedShouldBeTrue()
        {
            // Arrange
            var frame = Frame.Create();

            // Act
            frame.AddShot(10);

            // Assert
            frame.IsFinished.Should().BeTrue();
        }

        [Test]
        public void AddShot_WhenFrameIsFinished_ShouldThrowWhenAddingAnotherShot()
        {
            // Arrange
            var frame = Frame.Create();
            frame.AddShot(10);

            // Act
            Action act = () => frame.AddShot(1);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.FrameIsFinishedRuleText);
        }

        [Test]
        public void AddAllFrames_WhenAdding10Frames_ShouldSucceed()
        {
            // Arrange
            var frame = Frame.Create();
            var tenFrames = GetNumberOfFrames(10);

            // Act
            frame.AddAllFrames(tenFrames);

            // Assert
            frame.AllFrames.Count.Should().Be(10);
        }

        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(9)]
        [TestCase(11)]
        public void AddAllFrames_WhenAddingWrongNumberOfFrames_ShouldThrow(int numberOfFramesToAdd)
        {
            // Arrange
            var frame = Frame.Create();
            var frames = GetNumberOfFrames(numberOfFramesToAdd);

            // Act
            Action act = () => frame.AddAllFrames(frames);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.FrameCountMustBe10RuleText);
        }

        [Test]
        public void AddAllFrames_WhenAddingFramesTwice_ShouldThrow()
        {
            // Arrange
            var frame = CreateFrameWith10Frames();

            // Act
            Action act = () => frame.AddAllFrames(GetNumberOfFrames(10));

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage(ValidationRuleTextTemplates.CanOnlyAddFramesOnceRuleText);
        }

        private Frame CreateFrameWith10Frames()
        {
            var frames = GetNumberOfFrames(10);

            foreach (var frame in frames)
            {
                frame.AddAllFrames(frames);
            }

            return frames.First();
        }

        private List<Frame> GetNumberOfFrames(int numberOfFrames)
        {
            var frames = new List<Frame>();

            for (var i = 0; i < numberOfFrames; i++)
            {
                frames.Add(Frame.Create());
            }

            return frames;
        }
    }
}
