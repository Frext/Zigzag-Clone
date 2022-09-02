using UnityEngine;

[RequireComponent(typeof(PlatformPool))]
public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 firstBasePosition;
    [SerializeField] private Vector3 leftOffset;
    [SerializeField] private Vector3 forwardOffset;
    private Vector3 latestBasePosition;


    [Space]
    [Range(1, 10)]
    [SerializeField] private int maxPlatformsToLeft;

    [Range(1, 10)]
    [Tooltip("Keep in mind that the direction of the first platform is to the right.")]
    [SerializeField] private int maxPlatformsToRight;

    // Negative platform count represents left, positive platform count represents right.
    private int platformCountWithDirection;


    [Space]
    [SerializeField] private float spawnRepeatRate;

    [Header("Scripts")]
    [SerializeField] private PlatformPool platformPoolScript;

    private void Awake()
    {
        latestBasePosition = firstBasePosition;
    }

    void Start()
    {
        // This is set to 1 because the first platform is to right.
        SetPlatformCountWithDirection(1);

        PositionAllAvailablePlatforms();
        InvokeRepeating(nameof(PositionPlatform), 1f, spawnRepeatRate);
    }

    private void SetPlatformCountWithDirection(int val)
    {
        platformCountWithDirection = Mathf.Clamp(val, -maxPlatformsToLeft, maxPlatformsToRight);
    }

    private void PositionAllAvailablePlatforms()
    {
        // Position all platforms in the pool.
        for (int count = 0; count < platformPoolScript.PoolSize; count++)
        {

            PositionPlatform();
        }
    }

    private void PositionPlatform()
    {
        GameObject newPlatform = platformPoolScript.GetFromPoolOrNull();

        if (newPlatform == null)
            return;


        Vector3 randomPos = GetRandomPosition();
        newPlatform.transform.position = randomPos;

        SetBasePosition(randomPos);
    }

    private void SetBasePosition(Vector3 newPosition)
    {
        latestBasePosition = newPosition;
    }

    private Vector3 GetRandomPosition()
    {
        int randomDir = Random.Range(0, 2);
        Vector3 randomPos = Vector3.zero;

        if ((randomDir == 0 && CanPlacePlatformToLeft()) || !CanPlacePlatformToForward())
        {

            randomPos = latestBasePosition + leftOffset;

            SetPlatformCountWithDirection(platformCountWithDirection - 1);
        }
        else if ((randomDir == 1 && CanPlacePlatformToForward()) || !CanPlacePlatformToLeft())
        {
            randomPos = latestBasePosition + forwardOffset;

            SetPlatformCountWithDirection(platformCountWithDirection + 1);
        }

        return randomPos;
    }

    private bool CanPlacePlatformToForward()
    {
        if (platformCountWithDirection == maxPlatformsToRight)
        {
            return false;
        }

        return true;
    }

    private bool CanPlacePlatformToLeft()
    {
        if (platformCountWithDirection == -maxPlatformsToLeft)
        {
            return false;
        }

        return true;
    }
}
