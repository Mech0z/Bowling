using BowlingLib.Domain;

namespace BowlingLib.Repository
{
    public class BowlingGameRepository : IBowlingGameRepository
    {
        public Task<BowlingGame> GetGameAsync(Guid id)
        {
            return Task.FromResult(BowlingGame.Create());
        }

        public Task<List<BowlingGame>> GetAllGamesAsync()
        {
            return Task.FromResult(new List<BowlingGame> { BowlingGame.Create() });
        }

        public Task SaveGameAsync(BowlingGame bowlingGame)
        {
            return Task.CompletedTask;
        }
    }
}
