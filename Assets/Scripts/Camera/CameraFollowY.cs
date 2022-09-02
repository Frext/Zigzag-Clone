using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    private const float PLAYER_SPEED_CAMERA_SPEED_RATIO = 3.2f;
    

    [Tooltip("This script is used for getting the current speed of the player and adjusting the camera follow speed according to it.")]
    [SerializeField]
    private PlayerMovement playerMovementScript;

    void Update()
    {
        transform.Translate(playerMovementScript.speed / PLAYER_SPEED_CAMERA_SPEED_RATIO * Time.deltaTime * Vector3.up);
    }
}
