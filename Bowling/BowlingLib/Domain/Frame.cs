using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class Frame
    {
        private Frame()
        {
            PinsKnockedOver = new List<int>();
            AllFrames = new List<Frame>();
        }

        public static Frame Create()
        {
            return new Frame();
        }

        public void AddAllFrames(List<Frame> frames)
        {
            if (frames.Count != 10)
                throw new InvalidOperationException(ValidationRuleTextTemplates.FrameCountMustBe10RuleText);

            if (AllFrames.Any())
                throw new InvalidOperationException(ValidationRuleTextTemplates.CanOnlyAddFramesOnceRuleText);

            AllFrames.AddRange(frames);
        }

        public void AddShot(int pinsKnockedOver)
        {
            if (IsFinished)
                throw new InvalidOperationException(ValidationRuleTextTemplates.FrameIsFinishedRuleText);

            if (pinsKnockedOver > PinsLeft)
                throw new InvalidOperationException(ValidationRuleTextTemplates
                    .ImpossibleNumberOfPinsKnockedOverRuleText);

            PinsKnockedOver.Add(pinsKnockedOver);
        }

        public List<int> PinsKnockedOver { get; }

        public int? Points
        {
            get
            {
                if (IsLastFrame)
                {
                    return CalculatePointsForLastFrame();
                }

                if (IsStrike)
                {
                    return CalculatePointsForStrike();
                }

                if (IsSpare)
                {
                    return CalculatePointsForSpare();
                }

                if (IsFinished)
                    return PinsKnockedOver.Sum();

                return  null;
            }
        }

        private int? CalculatePointsForLastFrame()
        {
            throw new NotImplementedException();
        }

        private int? CalculatePointsForSpare()
        {
            if (NextFrame.PinsKnockedOver.Any())
                return 10 + NextFrame.PinsKnockedOver.First();

            return null;
        }

        private int? CalculatePointsForStrike()
        {
            if (NextFrame.IsStrike)
            {
                if (NextFrame.IsFinished && NextFrame.NextFrame.IsFinished)
                    return 10 + 10 + NextFrame.NextFrame.PinsKnockedOver.First();

                return null;
            }

            if (NextFrame.IsFinished)
                return 10 + NextFrame.PinsKnockedOver.Sum();

            return null;
        }

        public bool IsFinished => PinsKnockedOver.Count == 2 || IsStrike;

        private int PinsLeft => 10 - PinsKnockedOver.Sum();

        public bool IsStrike => PinsKnockedOver.Any() && PinsKnockedOver.First() == 10;

        public bool IsSpare => PinsKnockedOver.Count == 2 && PinsKnockedOver.Sum() == 10;

        public List<Frame> AllFrames { get; }

        public bool IsLastFrame => AllFrames.IndexOf(this) == 9;

        public Frame NextFrame
        {
            get
            {
                if (IsLastFrame)
                    throw new InvalidOperationException(ValidationRuleTextTemplates.CantRequestNextFrameFromLastFrameRuleText);
                return AllFrames[AllFrames.IndexOf(this) + 1];
            }
        }
    }
}
