using System.Collections;
using UnityEngine;

/// <summary>
/// The main game logic.
/// </summary>
public class Game : MonoBehaviour
{
    #region Variables

    public GameOverScript GameOverScript;

    #endregion

    #region GameUpdate

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    #endregion

    #region Game

    public void ProcessPlayerMove(string move)
    {
        var playerMove = GameManager.Game.ParsePlayerMove(move);

        AudioManager.Audio.PlayAudio(AudioManager.Audio.ClickSound);

        StartCoroutine(ProcessGameRound(playerMove));
    }

    private IEnumerator ProcessGameRound(RPSMoves playerMove)
    {
        StartCoroutine(ResetUIElementsForNewGameRound());

        // Show The Player Move
        UIManager.UI.ShowImage(UIManager.UI.PlayerMoveImages[playerMove]);

        var gameResult = GameManager.Game.CalculateResultOfGame();

        var aiMove = GameManager.Game.CalculateAiMove(playerMove, gameResult);

        // Show Ai Move
        UIManager.UI.ShowImage(UIManager.UI.AiMoveImages[aiMove]);

        yield return new WaitForSeconds(WaitConstants.WaitBeforeProcessGameResult);

        // Process Result of Game
        if (gameResult == GameResult.Win)
        {
            StartCoroutine(ProcessWin());
        }
        else if (gameResult == GameResult.Draw)
        {
            ProcessDraw();
        }
        else if (gameResult == GameResult.Loss)
        {
            StartCoroutine(ProcessLoss());
        }

        yield return new WaitForSeconds(WaitConstants.WaitBeforeReEnablingPlayerSelection);

        UIManager.UI.EnableButtons(UIManager.UI.PlayerSelectionButtons);
    }

    private IEnumerator ResetUIElementsForNewGameRound()
    {
        // Disable Player Selection
        UIManager.UI.DisableButtons(UIManager.UI.PlayerSelectionButtons);

        UIManager.UI.UpdateTextElement(
            UIManager.UI.ResultText,
            GameResultMessages.Empty);

        // Hide Last Move Images
        UIManager.UI.HideImages(UIManager.UI.MoveImages);

        // Play Move Video Animations
        UIManager.UI.ActivateGameObject(UIManager.UI.UserMoveVideo);
        UIManager.UI.ActivateGameObject(UIManager.UI.AiMoveVideo);

        // Stop the videos after they are finished
        yield return new WaitForSeconds(WaitConstants.WaitBeforeDeactivatingMoveVideos);
        UIManager.UI.DeactivateGameObject(UIManager.UI.UserMoveVideo);
        UIManager.UI.DeactivateGameObject(UIManager.UI.AiMoveVideo);
    }

    private IEnumerator ProcessWin()
    {
        GameManager.Game.IncrementScore();
        GameManager.Game.IncrementStreak();

        UIManager.UI.UpdateTextElement(
                UIManager.UI.ResultText,
                GameResultMessages.Win,
                ScoreColors.WinColor);

        AudioManager.Audio.PlayAudio(AudioManager.Audio.WinSound);

        yield return new WaitForSeconds(WaitConstants.WaitBeforeUpdatingScoreAndStreak);

        UIManager.UI.UpdateTextElement(
            UIManager.UI.ScoreText,
            GameManager.Game.Score.ToString(),
            GameManager.Game.GetScoreTextColor());

        UIManager.UI.UpdateTextElement(
            UIManager.UI.StreakText,
            GameManager.Game.Streak.ToString(),
            GameManager.Game.GetStreakTextColor());
    }

    private void ProcessDraw()
    {
        AudioManager.Audio.PlayAudio(AudioManager.Audio.DrawSound);

        UIManager.UI.UpdateTextElement(
            UIManager.UI.ResultText,
            GameResultMessages.Draw,
            ScoreColors.DrawColor);
    }

    private IEnumerator ProcessLoss()
    {
        GameManager.Game.DecrementScore();

        UIManager.UI.UpdateTextElement(
            UIManager.UI.ResultText,
            GameResultMessages.Loss,
            ScoreColors.LossColor);

        GameManager.Game.UpdateBestStreak();

        yield return new WaitForSeconds(WaitConstants.WaitBeforeProcessingGameOver);

        AudioManager.Audio.PlayAudio(AudioManager.Audio.LossSound);

        UIManager.UI.UpdateGameOverTextElement(
            UIManager.UI.GameOverScoreText,
            GameOverScreenConstants.GameOverScoreText,
            GameManager.Game.Score.ToString(),
            GameManager.Game.GetScoreTextColor(),
            GameOverScreenConstants.GameOverScoreTextSize);

        UIManager.UI.UpdateGameOverTextElement(
            UIManager.UI.WinningStreakText,
            GameOverScreenConstants.WinningStreakText,
            UIManager.UI.StreakText.text,
            GameManager.Game.GetStreakTextColor(GameManager.Game.Streak),
            GameOverScreenConstants.GameOverStreakTextSize);

        UIManager.UI.UpdateGameOverTextElement(
            UIManager.UI.BestStreakText,
            GameOverScreenConstants.BestStreakText,
            PlayerPrefs.GetInt(PlayerPrefNames.BestStreak).ToString(),
            GameManager.Game.GetStreakTextColor(PlayerPrefs.GetInt(PlayerPrefNames.BestStreak)),
            GameOverScreenConstants.GameOverBestStreakTextSize);

        GameManager.Game.ResetStreak();

        UIManager.UI.UpdateTextElement(
            UIManager.UI.ScoreText,
            GameManager.Game.Score.ToString(),
            GameManager.Game.GetScoreTextColor());

        UIManager.UI.UpdateTextElement(
            UIManager.UI.StreakText,
            GameManager.Game.Streak.ToString(),
            GameManager.Game.GetStreakTextColor());

        yield return new WaitForSeconds(WaitConstants.WaitBeforeGameOverScreen);

        GameOverScript.GameOver();
    }

    private void InitializeGame()
    {
        UIManager.UI.UpdateTextElement(
            UIManager.UI.ScoreText,
            GameManager.Game.Score.ToString(),
            GameManager.Game.GetScoreTextColor());

        UIManager.UI.UpdateTextElement(
            UIManager.UI.StreakText,
            GameManager.Game.Streak.ToString(),
            GameManager.Game.GetStreakTextColor());

        UIManager.UI.AssignUiElements();
        UIManager.UI.HideImages(UIManager.UI.MoveImages);
        UIManager.UI.DeactivateGameObject(UIManager.UI.UserMoveVideo);
        UIManager.UI.DeactivateGameObject(UIManager.UI.AiMoveVideo);
        UIManager.UI.UpdateTextElement(UIManager.UI.ResultText, GameResultMessages.Empty);

        GameOverScript.NewGame();
    }

    #endregion
}