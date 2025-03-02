using UnityEngine;

/// <summary>
/// Game over scene.
/// </summary>
public class GameOverScript : MonoBehaviour
{
    public Game Game;

    // Shows the game over scene.
    public void GameOver()
    {
        gameObject.SetActive(true);
    }

    // Hides the game over scene.
    public void StartGame()
    {
        gameObject.SetActive(false);
    }

    public void NewGame()
    {
        StartGame();
    }

    public void PlayAgain()
    {
        AudioManager.Audio.PlayAudio(AudioManager.Audio.PlayAgainSound);

        StartGame();
    }
}