using UnityEngine;

public class UIClickSFXHandler : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private SFXHandler sfxHandler;

    public void PlayUIInteractionSound()
    {
        sfxHandler.PlayAudioClip(SFXHandler.AudioClips.UIInteraction);
    }
}
