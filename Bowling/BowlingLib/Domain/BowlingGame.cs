namespace BowlingLib.Domain
{
    public class BowlingGame : IAggregateRoot
    {
        private BowlingGame()
        {
            Id = Guid.NewGuid();
        }

        public static BowlingGame Create()
        {
            return new BowlingGame();
        }

        public string PrintScore()
        {
            return string.Empty;
        }

        public Guid Id { get; }
    }

    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}