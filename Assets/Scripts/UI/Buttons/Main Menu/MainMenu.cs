using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] UIAnimationHandler animationHandler;

    public void GoToShopMenu()
    {
        animationHandler.DisableMainMenu();
        animationHandler.EnableShopMenu();
    }

    public void GoToHelpMenu()
    {
        animationHandler.DisableMainMenu();
        animationHandler.EnableHelpMenu();
    }
}
