namespace BowlingLib.Application
{
    public static class ValidationRuleTextTemplates
    {
        // BowlingGame
        public static string EmptyPlayerNameNotAllowedRuleText => "Empty name not allowed";
        public static string MaxPlayerNameLengthRuleText => "Max length of player name is 20";
        public static string NoPlayerNameAddedRuleText => "No player name added to the game.";
        public static string GameIsFinishedRuleText => "Game is finished";
        public static string CanNotAddShotBeforeNameIsAddedRuleText => "Must add name before rolling";

        // Frame
        public static string ImpossibleNumberOfPinsKnockedOverRuleText => "Too many pins knocked over";
        public static string FrameIsFinishedRuleText => "Cant add shots when frame is already finished";
        public static string FrameCountMustBe12RuleText => "Frame count must have total of 12, 10 normal + 2 possible bonus frames";
        public static string CanOnlyAddFramesOnceRuleText => "Can only add frames to a frame once";
        public static string CantRequestNextFrameFromLastFrameRuleText => "Cant request next frame from last frame";
        public static string InvalidFrameCombination => "Frame set must consist of 10 normal frames and 2 bonus frames";
    }
}
