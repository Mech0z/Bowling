namespace BowlingLib.Application
{
    public static class ValidationRuleTextTemplates
    {
        // Bowling game 
        public static string MaxPlayersAddedRuleText => "Max players per game is 8.";
        
        // Player
        public static string EmptyPlayerNameNotAllowedRuleText => "Empty name not allowed";
        public static string MaxPlayerNameLengthRuleText => "Max length of player name is 20";
        public static string NoPlayerNameAddedRuleText => "No player name added to the game.";
    }
}
