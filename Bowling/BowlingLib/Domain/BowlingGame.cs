using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class BowlingGame : IAggregateRoot
    {
        private BowlingGame()
        {
            Id = Guid.NewGuid();
            PlayerName = string.Empty;
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

        public Guid Id { get; }
        public string PlayerName { get; private set; }
    }
}