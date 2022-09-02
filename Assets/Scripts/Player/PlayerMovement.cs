using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    public float speed;
    [SerializeField]
    private float speedModifier;


    [Header("Scripts")]
    [Tooltip("This script is used for the camera to start following when the player starts moving.")]
    [SerializeField]
    private CameraFollowY cameraFollowScript;

    [Space]
    [Tooltip("This script is used to increment the score in every direction switch.")]
    [SerializeField]
    private PlayerScoreController playerScoreControllerScript;
    [Tooltip("This script is used to check if the player is grounded.")]
    [SerializeField]
    private PlayerLoseController playerLoseControllerScript;

    [Space]
    [SerializeField]
    private UIAnimationHandler animationHandler;
    [SerializeField]
    private SFXHandler sfxHandler;


    [HideInInspector]
    public bool IsFirstClick { get; private set; }


    private Vector3 currentDirection;
    private readonly Vector3 rightDir = Vector3.right;
    private readonly Vector3 forwardDir = Vector3.forward;

    void Start()
    {
        IsFirstClick = true;

        currentDirection = forwardDir;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && DidClickOnEmptySpace() && playerLoseControllerScript.IsPlayerGrounded)
        {

            StartCameraFollowingPlayer();

            SwitchDirection();


            PlayClickSound();

            IncrementScore();

            HandleStartAnimation();


            // The place of this line of code below matters for the functions above to work correctly.
            IsFirstClick = false;
        }

        if (!IsFirstClick)
        {

            MovePlayer();
            IncreaseSpeed();
        }
    }

    private bool DidClickOnEmptySpace()
    {
        // If the main menu is hidden before the first click (e.g. player go to shop menu, and help menu), don't move the player. 
        if (animationHandler.IsMainMenuHidden() && IsFirstClick)
            return false;


        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.CompareTag("No Direction Switch"))
                return false;
        }

        return true;
    }

    private void StartCameraFollowingPlayer() => cameraFollowScript.enabled = true;

    private void SwitchDirection()
    {
        if (currentDirection == rightDir)
        {
            currentDirection = forwardDir;
        }
        else
        {
            currentDirection = rightDir;
        }
    }

    private void HandleStartAnimation()
    {
        if (IsFirstClick)
        {

            animationHandler.DisableMainMenu();
            animationHandler.EnableScoreMenu();
        }
    }

    private void IncrementScore()
    {
        if (!IsFirstClick)
        {
            playerScoreControllerScript.IncrementScoreForSwitchingDirection();
        }
    }

    private void PlayClickSound()
    {
        // The first click sound is not played because UI interaction clip is played.
        if (!IsFirstClick)
        {

            sfxHandler.PlayAudioClip(SFXHandler.AudioClips.Click);
        }
    }

    private void MovePlayer() => transform.Translate(currentDirection * speed * Time.deltaTime);

    private void IncreaseSpeed() => speed += speedModifier * Mathf.Pow(Time.deltaTime, 2);
}