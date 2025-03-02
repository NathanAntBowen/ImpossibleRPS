using UnityEngine;

/// <summary>
/// Manager to handle audio.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Instance

    private static AudioManager instance;

    public static AudioManager Audio => instance;

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

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    #endregion

    #region Audio Elements

    public AudioClip ClickSound;
    public AudioClip WinSound;
    public AudioClip DrawSound;
    public AudioClip LossSound;
    public AudioClip PlayAgainSound;

    private AudioSource audioSource;

    #endregion

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}