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
        public static string FrameCountMustBe10RuleText => "Frame count must have total of 10";
        public static string CanOnlyAddFramesOnceRuleText => "Can only add frames to a frame once";
        public static string CantRequestNextFrameFromLastFrameRuleText => "Cant request next frame from last frame";
    }
}
