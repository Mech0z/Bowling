using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class Frame
    {
        private Frame()
        {
            PinsKnockedOver = new List<int>();
        }

        public static Frame Create()
        {
            return new Frame();
        }

        public void AddShot(int pinsKnockedOver)
        {
            if (FrameFinished)
                throw new InvalidOperationException(ValidationRuleTextTemplates.FrameIsFinishedRuleText);

            if (pinsKnockedOver > PinsLeft)
                throw new InvalidOperationException(ValidationRuleTextTemplates
                    .ImpossibleNumberOfPinsKnockedOverRuleText);
            PinsKnockedOver.Add(pinsKnockedOver);
        }

        public List<int> PinsKnockedOver { get; }

        public int Points => PinsKnockedOver.Sum();

        public bool FrameFinished => Points == 10 || PinsKnockedOver.Count == 2;

        private int PinsLeft => 10 - Points;
    }
}
