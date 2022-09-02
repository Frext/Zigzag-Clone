using UnityEngine;

public class AssignPlatformPool : MonoBehaviour
{
    [Tooltip("Assign this to recently created platforms for the platform fall script to work.")]
    [SerializeField]
    private PlatformPool platformPool;

    void Start()
    {
        AssignPlatformPoolForEachPlatform();

        DisableScript();
    }

    private void AssignPlatformPoolForEachPlatform()
    {
        foreach (PlatformFall platformFallScript in transform.GetComponentsInChildren<PlatformFall>(true))
        {
            platformFallScript.InitializePlatformPool(platformPool);
        }
    }

    private void DisableScript()
    {
        this.enabled = false;
    }
}
