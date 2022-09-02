using UnityEngine;

public class ButtonTurnAudioOnOff : MonoBehaviour
{
    [Header("Mute Button Images")]
    [SerializeField] GameObject soundOnImageGameObject;
    [SerializeField] GameObject soundOffImageGameObject;

    [Header("Scripts")]
    [SerializeField] SFXHandler sfxHandler;

    private void Start()
    {
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        if (sfxHandler.IsMuted())
        {

            soundOffImageGameObject.SetActive(true);
            soundOnImageGameObject.SetActive(false);
        }
        else
        {
            soundOnImageGameObject.SetActive(true);
            soundOffImageGameObject.SetActive(false);
        }
    }

    public void SwitchButtonState()
    {
        sfxHandler.SwitchAudioState();

        UpdateButtonImage();
    }
}
