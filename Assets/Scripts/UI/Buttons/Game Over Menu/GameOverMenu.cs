using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Scripts")]
    [Tooltip("This script is simply needed for not cutting the disappearing animation of the game over menu.")]
    [SerializeField] UIAnimationHandler animationHandler;
    [Tooltip("This game object is simply needed for not cutting the ui click sound.")]
    [SerializeField] SFXHandler sfxHandler;

    #region Retry Button
    public void ResetScene()
    {
        animationHandler.DisableGameOverMenu();

        DetachParent(sfxHandler.gameObject.transform);


        // Let the game objects do their job first, then destroy them.
        DontDestroyOnLoad(animationHandler.gameObject);
        DontDestroyOnLoad(sfxHandler.gameObject);


        StartCoroutine(DestroyDuplicatedGameObject
            (animationHandler.gameObject));

        StartCoroutine(DestroyDuplicatedGameObject
            (sfxHandler.gameObject));


        ReloadScene();
    }

    private void DetachParent(Transform childTransform)
    {
        if (transform.parent != null)
        {
            childTransform.parent.DetachChildren();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DestroyDuplicatedGameObject(GameObject duplicatedGameObject)
    {
        yield return new WaitForSeconds(.75f);

        Destroy(duplicatedGameObject);
    }

    #endregion
}
