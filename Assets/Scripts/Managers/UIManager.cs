using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager to handle UI logic.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Ui Elements

    public Text ScoreText;
    public Text GameOverScoreText;
    public Text WinningStreakText;
    public Text BestStreakText;
    public Text StreakText;
    public Text ResultText;

    public Image PlayerMoveRockImage;
    public Image PlayerMovePaperImage;
    public Image PlayerMoveScissorsImage;

    public Image AIMoveRockImage;
    public Image AIMovePaperImage;
    public Image AIMoveScissorsImage;

    public Button PlayerSelectionRockButton;
    public Button PlayerSelectionPaperButton;
    public Button PlayerSelectionScissorsButton;
    public Button PlayAgain;

    public GameObject UserMoveVideo;
    public GameObject AiMoveVideo;

    public RPSMoves playerMove;

    public Button[] PlayerSelectionButtons;
    public Image[] MoveImages;
    public GameObject[] MoveVideos;

    public readonly Dictionary<RPSMoves, Image> PlayerMoveImages = new();
    public readonly Dictionary<RPSMoves, Image> AiMoveImages = new();

    #endregion

    #region Instance

    private static UIManager instance;

    public static UIManager UI => instance;

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

    // Assigns the UI images to the user selections.
    public void AssignUiElements()
    {
        PlayerMoveImages[RPSMoves.Rock] = PlayerMoveRockImage;
        PlayerMoveImages[RPSMoves.Paper] = PlayerMovePaperImage;
        PlayerMoveImages[RPSMoves.Scissors] = PlayerMoveScissorsImage;

        AiMoveImages[RPSMoves.Rock] = AIMoveRockImage;
        AiMoveImages[RPSMoves.Paper] = AIMovePaperImage;
        AiMoveImages[RPSMoves.Scissors] = AIMoveScissorsImage;
    }

    public void DisableButtons(Button[] buttons)
    {
        SetButtonsInteractible(buttons, interactable: false);
    }

    public void EnableButtons(Button[] buttons)
    {
        SetButtonsInteractible(buttons, interactable: true);
    }

    public void HideImages(Image[] images)
    {
        foreach(Image image in images)
        {
            SetImageVisibility(image, visible: false);
        }
    }

    public void ShowImage(Image image)
    {
        SetImageVisibility(image, visible: true);
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        SetGameObjectActive(gameObject, active: true);
    }

    public void DeactivateGameObject(GameObject gameObject)
    {
        SetGameObjectActive(gameObject, active: false);
    }

    public void UpdateTextElement(Text textElement, string text, Color? color = null, int? fontSize = null)
    {
        textElement.text = text;

        if (color.HasValue)
        {
            textElement.color = color.Value;
        }

        if (fontSize.HasValue)
        {
            textElement.fontSize = fontSize.Value;
        }
    }

    public void UpdateGameOverTextElement(Text textElement, string text, string value, Color color, int fontSize)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        textElement.text = $"{text} <color=#{hexColor}><size={fontSize}>{value}</size></color>";
    }

    private void SetButtonsInteractible(Button[] buttons, bool interactable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = interactable;
        }
    }

    private void SetGameObjectActive(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }

    private void SetImageVisibility(Image image, bool visible)
    {
        image.enabled = visible;
    }
}