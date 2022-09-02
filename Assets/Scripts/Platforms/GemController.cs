using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem gemCollectParticleSystem;

    [SerializeField] private GameObject gemCollectText;

    public void PlayGemCollectParticleEffect()
    {
        gemCollectParticleSystem.Play();
    }

    public void EnableGemCollectText()
    {
        gemCollectText.SetActive(true);
    }

    // Don't destroy the gem because its platform is going to return to the platform pool.
    public void HideGem()
    {
        gameObject.SetActive(false);
    }
}
