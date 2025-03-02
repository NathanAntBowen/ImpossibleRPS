using System.Collections.Generic;

/// <summary>
/// Config to determine game moves and result probabilities.
/// </summary>
public static class GameResultConfig
{
    // AI Moves based on game result
    public static RPSMoves[] AiWinMoves = { RPSMoves.Scissors, RPSMoves.Rock, RPSMoves.Paper };
    public static RPSMoves[] AiLoseMoves = { RPSMoves.Paper, RPSMoves.Scissors, RPSMoves.Rock };

    // Win, Draw & Loss probabilities based on the streak level
    public static readonly Dictionary<StreakLevel, ResultProbabilities> GameResultProbabilities = new()
    {
        { StreakLevel.None, new ResultProbabilities(winProbability: 8, drawProbability: 1, lossProbability: 1) },
        { StreakLevel.Small, new ResultProbabilities(winProbability: 4, drawProbability: 4, lossProbability: 2) },
        { StreakLevel.Medium, new ResultProbabilities(winProbability: 3, drawProbability: 3, lossProbability: 4) },
        { StreakLevel.Large, new ResultProbabilities(winProbability: 2, drawProbability: 2, lossProbability: 6) },
        { StreakLevel.Impossible, new ResultProbabilities(winProbability: 1, drawProbability: 1, lossProbability: 8) }
    };
}