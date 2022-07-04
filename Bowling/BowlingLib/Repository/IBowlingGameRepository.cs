using BowlingLib.Domain;

namespace BowlingLib.Repository;

public interface IBowlingGameRepository
{
    Task<BowlingGame> GetGameAsync(Guid id);
    Task<List<BowlingGame>> GetAllGamesAsync();
    Task SaveGameAsync(BowlingGame bowlingGame);
}