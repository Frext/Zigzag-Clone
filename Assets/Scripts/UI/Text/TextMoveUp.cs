using UnityEngine;

public class TextMoveUp : MonoBehaviour
{
    [SerializeField] Animator textAnimator;

    // This will be called whenever the gem is re-added to the pool.
    void OnEnable()
    {
        textAnimator.SetTrigger("moveTextUp");
    }
}
