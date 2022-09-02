using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinController : MonoBehaviour
{
    [SerializeField] private Image playerSkinImage;

    [Header("Scripts")]
    [SerializeField] private DataManager dataManager;
    [Space]
    [SerializeField] private ShopItemManager shopItemManager;

    void Start()
    {
        SetPlayerSkin(shopItemManager.GetSpriteFromGuid(dataManager.latestData.SelectedPlayerSkinGuid));
    }

    public void SetPlayerSkin(Sprite newPlayerSkin)
    {
        playerSkinImage.sprite = newPlayerSkin;    
    }
}
