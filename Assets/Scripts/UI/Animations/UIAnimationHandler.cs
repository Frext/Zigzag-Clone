using System;
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour
{
    [Header("UI Animators")]
    [SerializeField] Animator mainMenuAnimator;
    [SerializeField] Animator gameOverMenuAnimator;
    [SerializeField] Animator shopMenuAnimator;
    [SerializeField] Animator helpMenuAnimator;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject scoreMenu;
    
    public void EnableMainMenu()
    {
        mainMenuAnimator.gameObject.SetActive(true);

        mainMenuAnimator.SetBool("isVisible", true);
    }

    public void DisableMainMenu()
    {
        mainMenuAnimator.SetBool("isVisible", false);
    }

    public bool IsMainMenuHidden()
    {
        return !mainMenuAnimator.GetBool("isVisible");
    }

    public void EnableGameOverMenu()
    {
        gameOverMenuAnimator.gameObject.SetActive(true);

        gameOverMenuAnimator.SetBool("isVisible", true);
    }

    public void DisableGameOverMenu()
    {
        gameOverMenuAnimator.SetBool("isVisible", false);
    }

    public void EnableNewHighScoreText()
    {
        gameOverMenuAnimator.gameObject.SetActive(true);

        gameOverMenuAnimator.SetTrigger("highScore");
    }

    public void EnableScoreMenu()
    {
        scoreMenu.SetActive(true);
    }

    public void DisableScoreMenu()
    {
        scoreMenu.SetActive(false);
    }

    public void EnableShopMenu()
    {
        shopMenuAnimator.gameObject.SetActive(true);

        shopMenuAnimator.SetBool("isVisible", true);
        cameraAnimator.SetBool("shopMenu_visible", true);
    }

    public void DisableShopMenu()
    {
        shopMenuAnimator.SetBool("isVisible", false);
        cameraAnimator.SetBool("shopMenu_visible", false);
    }

    public void EnableHelpMenu()
    {
        helpMenuAnimator.gameObject.SetActive(true);

        helpMenuAnimator.SetBool("isVisible", true);
        cameraAnimator.SetBool("helpMenu_visible", true);
    }

    public void DisableHelpMenu()
    {
        helpMenuAnimator.SetBool("isVisible", false);
        cameraAnimator.SetBool("helpMenu_visible", false);
    }
}
