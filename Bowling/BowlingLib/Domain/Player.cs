using BowlingLib.Application;

namespace BowlingLib.Domain
{
    public class Player
    {
        public string Name { get; }

        private Player(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }

        public static Player Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException(ValidationRuleTextTemplates.EmptyPlayerNameNotAllowedRuleText);
            if (name.Length > 20)
                throw new InvalidOperationException(ValidationRuleTextTemplates.MaxPlayerNameLengthRuleText);
            return new Player(name);
        }
    }
}
