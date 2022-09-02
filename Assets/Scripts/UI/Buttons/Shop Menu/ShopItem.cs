using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemVariable shopItemVariable;

    [Header("Button States")]
    [Space]
    [SerializeField] private GameObject notBought;
    [SerializeField] private GameObject bought;

    [Space]
    [SerializeField] private Image boughtStateBackgroundImage;
    [SerializeField] private Image skinImage;
    [SerializeField] private TextMeshProUGUI gemPriceTextMeshPro;

    [Header("Scripts")]
    [SerializeField] ShopItemManager shopItemManager;


    void Start()
    {
        AssignShopItem();
    }

    private void AssignShopItem()
    {
        gemPriceTextMeshPro.text = shopItemVariable.gemPrice.ToString();

        skinImage.sprite = shopItemVariable.itemSprite;
    }

    #region Button On Click Commands
    public void BuyShopItem()
    {
        shopItemManager.BuyShopItem(shopItemVariable);
    }

    public void SetAsDefaultPlayerSkin()
    {
        shopItemManager.SetAsDefaultPlayerSkin(shopItemVariable);
    }
    #endregion

    public void RevealShopItem()
    {
        notBought.SetActive(false);
        bought.SetActive(true);
    }

    public string GetShopItemGUID()
    {
        return shopItemVariable.guid;
    }

    public Sprite GetShopItemSprite()
    {
        return shopItemVariable.itemSprite;
    }

    public Image GetBoughtStateButtonBackground()
    {
        return boughtStateBackgroundImage;
    }
}
