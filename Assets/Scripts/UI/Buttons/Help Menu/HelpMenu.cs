using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] UIAnimationHandler animationHandler;

    public void GoToMainMenu()
    {
        animationHandler.EnableMainMenu();
        animationHandler.DisableHelpMenu();
    }
}
