namespace BowlingLib.Application
{
    public static class ValidationRuleTextTemplates
    {
        // Player
        public static string EmptyPlayerNameNotAllowedRuleText => "Empty name not allowed";
        public static string MaxPlayerNameLengthRuleText => "Max length of player name is 20";
        public static string NoPlayerNameAddedRuleText => "No player name added to the game.";

        // Frame
        public static string ImpossibleNumberOfPinsKnockedOverRuleText => "Too many pins knocked over";
        public static string FrameIsFinishedRuleText => "Cant add shots when frame is already finished";
    }
}
