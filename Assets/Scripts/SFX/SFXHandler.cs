using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("Audio Source Properties")]
    [Range(0, 1)]
    [SerializeField]
    float generalVolume = 0.5f;

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip uiInteractionSound;
    [SerializeField]
    AudioClip clickSound;
    [SerializeField]
    AudioClip gemCollectSound;
    [SerializeField]
    AudioClip loseGameSound;
    [SerializeField]
    AudioClip newHighScoreSound;

    [Header("Scripts")]
    [SerializeField] DataManager dataManager;

    #region Data Properties
    private bool _isAudioOn;
    private bool IsAudioOn
    {
        get { return _isAudioOn; }
        set
        {
            _isAudioOn = value;
            SaveAudioState();
        }
    }

    private void SaveAudioState()
    {
        DataContainer dc = dataManager.latestData;
        
        if(dc.IsAudioOn == IsAudioOn)
            return;


        dc.IsAudioOn = IsAudioOn;

        dataManager.SaveData(dc);
    }
    #endregion

    public enum AudioClips
    {
        UIInteraction,
        Click,
        GemCollect,
        LoseGame,
        NewHighScore
    }

    void Start()
    {
        audioSource.volume = generalVolume;

        LoadData();
    }

    private void LoadData()
    {
        IsAudioOn = dataManager.latestData.IsAudioOn;
    }

    public void PlayAudioClip(AudioClips audioClip)
    {
        if (!IsAudioOn)
            return;

        if (audioClip == AudioClips.UIInteraction)
        {

            audioSource.PlayOneShot(uiInteractionSound);
        }
        else if (audioClip == AudioClips.Click)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else if (audioClip == AudioClips.GemCollect)
        {
            audioSource.PlayOneShot(gemCollectSound);
        }
        else if (audioClip == AudioClips.LoseGame)
        {
            audioSource.PlayOneShot(loseGameSound);
        }
        else if (audioClip == AudioClips.NewHighScore)
        {
            audioSource.PlayOneShot(newHighScoreSound);
        }
    }

    public bool IsMuted()
    {
        if (IsAudioOn)
        {

            return false;
        }

        return true;
    }

    public void SwitchAudioState()
    {
        IsAudioOn = !IsAudioOn;
    }
}
