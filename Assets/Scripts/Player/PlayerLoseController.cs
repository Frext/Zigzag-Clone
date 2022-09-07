using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLoseController : MonoBehaviour
{
    [Header("Lose Conditions")]
    [SerializeField] private float loseY;

    [Header("Scripts")]
    [Tooltip("This script will disable camera following if the player is falling down.")]
    [SerializeField] private CameraFollowY cameraFollowScript;
    [Space]
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private PlayerScoreController playerScoreControllerScript;
    [Space]
    [SerializeField] private UIAnimationHandler animationHandler;
    [SerializeField] private SFXHandler sfxHandler;

    public bool IsPlayerGrounded { get; private set; }

    void Update()
    {
        UpdateGroundState();    
    }

    private void UpdateGroundState()
    {
        if (transform.position.y > loseY)
        {
            IsPlayerGrounded = true;
        }
        else
        {
            LoseGame();

            IsPlayerGrounded = false;
        }
    }

    private void LoseGame()
    {
        StopCameraFollowingPlayer();


        IncreaseGemCountByScore();

        HandleAnimationByScore();
        PlaySoundByScore();

        // Don't call this before the animations and sounds are handled.
        // Because they check if the current score is greater than the latest high score.
        UpdateHighScore();


        // Disable the current script to prevent calling the functions above over and over.
        DisableScript();
    }

    private void StopCameraFollowingPlayer()
    {
        cameraFollowScript.enabled = false;
    }

    private void IncreaseGemCountByScore()
    {
        playerScoreControllerScript.IncreaseGemCountByScore();
    }

    private void HandleAnimationByScore()
    {
        animationHandler.DisableScoreMenu();
        animationHandler.EnableGameOverMenu();

        if (playerScoreControllerScript.IsNewHighScore())
        {

            animationHandler.EnableNewHighScoreText();
        }
    }

    private void PlaySoundByScore()
    {
        if (playerScoreControllerScript.IsNewHighScore())
        {

            sfxHandler.PlayAudioClip(SFXHandler.AudioClips.NewHighScore);
        }
        else
        {
            sfxHandler.PlayAudioClip(SFXHandler.AudioClips.LoseGame);
        }
    }

    private void UpdateHighScore()
    {
        playerScoreControllerScript.UpdateHighScore();
    }

    private void DisableScript()
    {
        this.enabled = false;
    }
}
