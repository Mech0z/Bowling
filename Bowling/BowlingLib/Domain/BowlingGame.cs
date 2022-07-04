using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class BowlingGame : IAggregateRoot
    {
        private BowlingGame()
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
        }

        public static BowlingGame Create()
        {
            return new BowlingGame();
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count == 8)
                throw new InvalidOperationException(ValidationRuleTextTemplates.MaxPlayersAddedRuleText);

            Players.Add(player);
        }

        public List<string> GetScoreLines()
        {
            if (Players.Count == 0) return new List<string>{ "No players added to the game." };

            return Players.Select(player => $"{player.Name}: 0").ToList();
        }

        public Guid Id { get; }
        public List<Player> Players { get; }
    }
}