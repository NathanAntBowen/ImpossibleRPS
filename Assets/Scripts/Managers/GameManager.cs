using System;
using UnityEngine;

/// <summary>
/// Manager to handle result, score & streak logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Instance

    private static GameManager instance;

    public static GameManager Game => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private int score;
    private int streak;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt(PlayerPrefNames.Score);
        ResetStreak();
    }

    public void SetPlayerPref(string pref, int value)
    {
        PlayerPrefs.SetInt(pref, value);
    }

    public int Score
    {
        get => score;
        private set
        {
            score = value;
            SetPlayerPref(PlayerPrefNames.Score, score);
        }
    }

    public int Streak
    {
        get => streak;
        private set
        {
            streak = value;
            SetPlayerPref(PlayerPrefNames.Streak, streak);
        }
    }

    public void IncrementScore()
    {
        if (score < int.MaxValue)
        {
            Score++;
        }
    }

    public void DecrementScore()
    {
        if (score > 0)
        {
            Score--;
        }
    }

    public void IncrementStreak()
    {
        Streak++;
    }

    public void ResetStreak()
    {
        Streak = 0;
    }

    public RPSMoves ParsePlayerMove(string move)
    {
        if (Enum.TryParse(typeof(RPSMoves), move, out var rpsMove))
        {
            return (RPSMoves)rpsMove;
        }

        return RPSMoves.Rock; // Default invalid moves to Rock.
    }

    public GameResult CalculateResultOfGame()
    {
        // Generate a random number between 1 - 10
        var randomNumber = UnityEngine.Random.Range(minInclusive: 1, maxExclusive: 11);

        var streakLevel = GetCurrentStreakLevel(Streak);

        // Win, Draw & Loss probabilities based on the streak level
        var gameResultProbability = GameResultConfig.GameResultProbabilities[streakLevel];

        var winThreshold = gameResultProbability.WinProbability;
        var drawThreshold = winThreshold + gameResultProbability.DrawProbability;

        // Calculate result based on random number & result probabilities based on streak level
        if (randomNumber <= winThreshold)
        {
            return GameResult.Win;
        }
        else if (randomNumber <= drawThreshold)
        {
            return GameResult.Draw;
        }
        else
        {
            return GameResult.Loss;
        }
    }

    // Ai move is determined based on the game result and players move.
    // Ai move does not affect result; it is calculated after result for display purposes.
    public RPSMoves CalculateAiMove(RPSMoves playerMove, GameResult gameResult)
    {
        var aiMove = RPSMoves.Unknown;

        if (gameResult is GameResult.Win)
        {
            aiMove = GameResultConfig.AiWinMoves[(int)playerMove];
        }
        else if (gameResult is GameResult.Draw)
        {
            aiMove = playerMove;
        }
        else if (gameResult is GameResult.Loss)
        {
            aiMove = GameResultConfig.AiLoseMoves[(int)playerMove];
        }

        return aiMove;
    }

    public void UpdateBestStreak()
    {
        var currentBestStreak = PlayerPrefs.GetInt(PlayerPrefNames.BestStreak);

        if (currentBestStreak < Streak)
        {
            SetPlayerPref(PlayerPrefNames.BestStreak, Streak);
        }
    }

    public Color GetScoreTextColor()
    {
        if (Score <= ScoreConstants.NoScore)
        {
            return ScoreColors.NoScore;
        }
        else if (Score < ScoreConstants.SmallScore)
        {
            return ScoreColors.SmallScore;
        }
        else if (Score < ScoreConstants.MediumScore)
        {
            return ScoreColors.MediumScore;
        }
        else if (Score < ScoreConstants.LargeScore)
        {
            return ScoreColors.LargeScore;
        }
        else if (Score < ScoreConstants.XlScore)
        {
            return ScoreColors.XlScore;
        }
        else if (Score < ScoreConstants.XxlScore)
        {
            return ScoreColors.XxlScore;
        }
        else if (Score < ScoreConstants.XxxlScore)
        {
            return ScoreColors.XxxlScore;
        }
        else if (Score < ScoreConstants.ImpossibleScore)
        {
            return ScoreColors.ImpossibleScore;
        }

        return ScoreColors.NoScore; // Default to No Score color.
    }

    public Color GetStreakTextColor(int? streak = null)
    {
        streak ??= Streak;

        if (streak == StreakConstants.NoStreak)
        {
            return ScoreColors.NoScore;
        }
        else if (streak <= StreakConstants.SmallStreak)
        {
            return ScoreColors.SmallScore;
        }
        else if (streak <= StreakConstants.MediumStreak)
        {
            return ScoreColors.MediumScore;
        }
        else if (streak <= StreakConstants.LargeStreak)
        {
            return ScoreColors.LargeScore;
        }
        else if (streak <= StreakConstants.XlStreak)
        {
            return ScoreColors.XlScore;
        }
        else if (streak <= StreakConstants.XxlStreak)
        {
            return ScoreColors.XxlScore;
        }
        else if (streak <= StreakConstants.XxxlStreak)
        {
            return ScoreColors.XxxlScore;
        }
        else if (streak <= StreakConstants.ImpossibleStreak)
        {
            return ScoreColors.ImpossibleScore;
        }

        return ScoreColors.NoScore; // Default to No Score color.
    }

    // Calculates streak level using streak threshold.
    private static StreakLevel GetCurrentStreakLevel(int currentStreak)
    {
        
        if (currentStreak < (int)StreakLevel.Small)
        {
            return StreakLevel.None;
        }
        else if (currentStreak < (int)StreakLevel.Medium)
        {
            return StreakLevel.Small;
        }
        else if (currentStreak < (int)StreakLevel.Large)
        {
            return StreakLevel.Medium;
        }
        else if (currentStreak < (int)StreakLevel.Impossible)
        {
            return StreakLevel.Large;
        }
        else
        {
            return StreakLevel.Impossible;
        }
    }

    #region Testing

    // Reset Game for Testing Purposes
    private void ResetGame()
    {
        score = 0;
        streak = 0;
        PlayerPrefs.SetInt(PlayerPrefNames.Score, 0);
        PlayerPrefs.SetInt(PlayerPrefNames.Streak, 0);
        PlayerPrefs.SetInt(PlayerPrefNames.BestStreak, 0);
    }

    #endregion
}