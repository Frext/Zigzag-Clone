using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlatformFall : MonoBehaviour
{
    [Header("Platform Fall Conditions")]
    [SerializeField] private float fallDistance;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float fallUntilYCoordinate;

    [SerializeField] private bool shouldBeDestroyedOnFall;


    private bool didPlayerPassPlatform;

    private Renderer playerRenderer;

    private Vector3 playerPosition;

    // The player transform variable is used for calculating the distance between the player and the platform.
    // And it's initialized by another script for this purpose.
    private Transform playerTransform;

    // This is used for re-adding the fallen platforms to the pool.
    // And it's initialized by another script for this purpose.
    private PlatformPool platformPoolScript;

    void Start()
    {
        didPlayerPassPlatform = false;

        playerRenderer = GetComponent<Renderer>();
    }

    // This will be called when the platform is re-added to the pool.
    void OnEnable()
    {
        didPlayerPassPlatform = false;
    }

    void Update()
    {
        playerPosition = new Vector3(playerTransform.transform.position.x, 0, playerTransform.transform.position.z);

        if ((Vector3.Distance(transform.position, playerPosition) > fallDistance) && didPlayerPassPlatform)
        {

            MakePlatformFall();

            HandlePlatformOutOfScreen();
        }
    }

    private void MakePlatformFall()
    {
        if (transform.position.y > fallUntilYCoordinate)
        {

            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            // Make the fallen platforms position smoother rather than bumpy.
            transform.position = new Vector3(transform.position.x, fallUntilYCoordinate, transform.position.z);
        }
    }

    private void HandlePlatformOutOfScreen()
    {
        if (playerRenderer.isVisible)
            return;


        if (shouldBeDestroyedOnFall)
        {

            Destroy(gameObject);
        }
        else
        {
            platformPoolScript.AddToPool(gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            didPlayerPassPlatform = true;
        }
    }

    public void InitializePlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }

    public void InitializePlatformPool(PlatformPool platformPool)
    {
        platformPoolScript = platformPool;
    }
}
