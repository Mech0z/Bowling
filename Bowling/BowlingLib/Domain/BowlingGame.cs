using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class BowlingGame : IAggregateRoot
    {
        public Guid Id { get; }
        public string PlayerName { get; private set; }
        public List<Frame> Frames { get; private set; }

        private BowlingGame()
        {
            Id = Guid.NewGuid();
            PlayerName = string.Empty;
            Frames = new List<Frame>();
            CreateFramesForNewGame();
        }

        public static BowlingGame Create()
        {
            return new BowlingGame();
        }

        public void AddPlayerName(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new InvalidOperationException(ValidationRuleTextTemplates.EmptyPlayerNameNotAllowedRuleText);
            if (playerName.Length > 20)
                throw new InvalidOperationException(ValidationRuleTextTemplates.MaxPlayerNameLengthRuleText);

            PlayerName = playerName;
        }

        public string GetScoreBoard()
        {
            if (PlayerName == string.Empty)
                throw new InvalidOperationException(ValidationRuleTextTemplates.NoPlayerNameAddedRuleText);

            return $"{PlayerName}: 0";
        }

        public void AddShot(int pinsKnockedOver)
        {
            var currentFrame = GetCurrentFrame();
            currentFrame.AddShot(pinsKnockedOver);
        }

        public int? GetTotalPoints()
        {
            int? totalPoints = null;

            foreach (var frame in Frames)
            {
                if (frame.IsFinished && frame.Points is not null)
                {
                    totalPoints ??= 0;
                    totalPoints += frame.Points;
                }
            }

            return totalPoints;
        }

        private Frame GetCurrentFrame()
        {
            foreach (var frame in Frames)
            {
                if (frame.IsFinished is false)
                    return frame;
            }

            throw new ArgumentOutOfRangeException(ValidationRuleTextTemplates.GameIsFinishedRuleText);
        }

        private void CreateFramesForNewGame()
        {
            var frames = new List<Frame>();

            for (var i = 0; i < 10; i++)
            {
                var frame = Frame.Create();
                frames.Add(frame);
            }
            frames.Add(Frame.CreateBonusFrame());
            frames.Add(Frame.CreateBonusFrame());

            frames.ForEach(x => x.AddAllFrames(frames));

            Frames = frames;
        }
    }
}