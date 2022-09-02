using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] UIAnimationHandler animationHandler;

    public void GoToMainMenu()
    {
        animationHandler.DisableShopMenu();
        animationHandler.EnableMainMenu();
    }
}
