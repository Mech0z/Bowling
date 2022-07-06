using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class Frame
    {
        private Frame(bool bonusFrame = false)
        {
            PinsKnockedOver = new List<int>();
            AllFrames = new List<Frame>();
            BonusFrame = bonusFrame;
        }

        public static Frame Create()
        {
            return new Frame();
        }

        public static Frame CreateBonusFrame()
        {
            return new Frame(true);
        }

        public void AddAllFrames(List<Frame> frames)
        {
            if (frames.Count != 12)
                throw new InvalidOperationException(ValidationRuleTextTemplates.FrameCountMustBe12RuleText);

            if (AllFrames.Any())
                throw new InvalidOperationException(ValidationRuleTextTemplates.CanOnlyAddFramesOnceRuleText);

            if(frames.Count(x => x.BonusFrame == false) != 10 && frames.Count(x => x.BonusFrame) != 2)
                throw new InvalidOperationException(ValidationRuleTextTemplates.InvalidFrameCombination);

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

        public List<Frame> AllFrames { get; }

        public List<int> PinsKnockedOver { get; }
        
        public bool BonusFrame { get; }

        public int? Points
        {
            get
            {
                if (BonusFrame)
                    return null;
                
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
        
        private bool SecondBonusFrameIndex => AllFrames.IndexOf(this) == 11;

        public Frame NextFrame
        {
            get
            {
                if (SecondBonusFrameIndex)
                    throw new InvalidOperationException(ValidationRuleTextTemplates.CantRequestNextFrameFromLastFrameRuleText);
                return AllFrames[AllFrames.IndexOf(this) + 1];
            }
        }
    }
}
