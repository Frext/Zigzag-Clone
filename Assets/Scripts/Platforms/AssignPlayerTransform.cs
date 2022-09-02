using System;
using UnityEngine;

public class AssignPlayerTransform : MonoBehaviour
{
    [Tooltip("Assign this to recently created platforms for the platform fall script to work.")]
    [SerializeField]
    private Transform playerTransform;

    // We call this function in start because the pool is created in the Awake method.
    void Start()
    {
        AssignPlayerTransformForEachPlatform();

        DisableScript();
    }

    private void AssignPlayerTransformForEachPlatform()
    {
        foreach (PlatformFall platformFallScript in transform.GetComponentsInChildren<PlatformFall>(true))
        {
            platformFallScript.InitializePlayerTransform(playerTransform);
        }
    }

    private void DisableScript()
    {
        this.enabled = false;
    }
}
