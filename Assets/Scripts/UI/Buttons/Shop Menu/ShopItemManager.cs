using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
    [SerializeField] List<Button> itemButtons = new List<Button>();

    [Header("Button Bought State Colors")]
    [SerializeField] Color boughtButtonStateColorDefault;
    [SerializeField] Color boughtButtonStateColorOnSelect;

    [Header("Scripts")]
    [SerializeField] PlayerSkinController playerSkinControllerScript;
    [SerializeField] PlayerScoreController playerScoreControllerScript;
    [Space]
    [SerializeField] SFXHandler sfxHandler;
    [SerializeField] DataManager dataManager;

    #region Data Properties

    private List<DataContainer.ShopItemData> _boughtShopItemsList = new List<DataContainer.ShopItemData>();
    public List<DataContainer.ShopItemData> BoughtShopItemsList
    {
        get { return _boughtShopItemsList; }
        set
        {
            _boughtShopItemsList = value;
            SaveBoughtItemsList();
        }
    }

    private void SaveBoughtItemsList()
    {
        DataContainer dc = dataManager.latestData;

        if (dc.BoughtShopItemsList == BoughtShopItemsList)
            return;

        dc.BoughtShopItemsList = BoughtShopItemsList;

        dataManager.SaveData(dc);
    }


    private string _selectedPlayerSkinGuid;
    public string SelectedPlayerSkinGuid
    {
        get { return _selectedPlayerSkinGuid; }
        set
        {
            _selectedPlayerSkinGuid = value;

            SetPlayerSkinBySelected();
            SaveSelectedPlayerSkinGuid();
        }
    }

    private void SetPlayerSkinBySelected()
    {
        playerSkinControllerScript.SetPlayerSkin(
            GetSpriteFromGuid(SelectedPlayerSkinGuid));
    }

    private void SaveSelectedPlayerSkinGuid()
    {
        DataContainer dc = dataManager.latestData;

        if (dc.SelectedPlayerSkinGuid == SelectedPlayerSkinGuid)
            return;

        dc.SelectedPlayerSkinGuid = SelectedPlayerSkinGuid;

        dataManager.SaveData(dc);
    }

    #endregion

    void Start()
    {
        LoadData();

        RevealBoughtShopItems();
    }

    private void LoadData()
    {
        BoughtShopItemsList = dataManager.latestData.BoughtShopItemsList;

        SelectedPlayerSkinGuid = dataManager.latestData.SelectedPlayerSkinGuid;
    }

    private void RevealBoughtShopItems()
    {
        foreach (Button itemButton in itemButtons)
        {

            ShopItem shopItemScript = itemButton.GetComponent<ShopItem>();

            if (shopItemScript == null)
                continue;


            if (IsShopItemInBoughtItemsList(shopItemScript.GetShopItemGUID()))
            {

                shopItemScript.RevealShopItem();
            }

            if (shopItemScript.GetShopItemGUID() == dataManager.latestData.SelectedPlayerSkinGuid)
            {

                ChangeButtonColorToSelected(shopItemScript.GetBoughtStateButtonBackground());
            }
        }
    }

    private bool IsShopItemInBoughtItemsList(string shopItemGUID)
    {
        foreach (DataContainer.ShopItemData boughtShopItem in BoughtShopItemsList)
        {
            if (boughtShopItem.Guid == shopItemGUID)
            {
                return true;
            }
        }

        return false;
    }

    private void ChangeButtonColorToSelected(Image buttonImage)
    {
        buttonImage.color = boughtButtonStateColorOnSelect;
    }

    public void BuyShopItem(ShopItemVariable shopItemVariable)
    {
        if (!HasSufficientGemsToBuy(shopItemVariable.gemPrice) || IsShopItemInBoughtItemsList(shopItemVariable.guid))
            return;

        BoughtShopItemsList.Add(
            new DataContainer.ShopItemData(shopItemVariable.guid));

        RevealBoughtShopItems();


        DecreaseGemCount(shopItemVariable.gemPrice);
    }

    private bool HasSufficientGemsToBuy(int gemPrice)
    {
        return dataManager.latestData.GemCount >= gemPrice;
    }

    private void DecreaseGemCount(int gemPrice)
    {
        playerScoreControllerScript.DecreaseGems(gemPrice);
    }

    public void SetAsDefaultPlayerSkin(ShopItemVariable shopItemVariable)
    {
        if (!IsShopItemInBoughtItemsList(shopItemVariable.guid))
            return;

        SelectedPlayerSkinGuid = shopItemVariable.guid;

        SetAllButtonsColorToDefault();
        RevealBoughtShopItems();


        sfxHandler.PlayAudioClip(SFXHandler.AudioClips.UIInteraction);
    }

    private void SetAllButtonsColorToDefault()
    {
        foreach (Button itemButton in itemButtons)
        {
            itemButton.GetComponent<ShopItem>().GetBoughtStateButtonBackground().color = boughtButtonStateColorDefault;
        }
    }

    public Sprite GetSpriteFromGuid(string guid)
    {
        if (!IsShopItemInBoughtItemsList(guid))
            return null;

        foreach (Button itemButton in itemButtons)
        {
            ShopItem shopItemScript = itemButton.GetComponent<ShopItem>();

            if (shopItemScript.GetShopItemGUID() == guid)
            {
                return shopItemScript.GetShopItemSprite();
            }
        }

        // The line below is just for the semantics.
        return null;
    }
}
