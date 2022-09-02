using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShopItemVariable), menuName = "Scriptable Objects/" + nameof(ShopItemVariable), order = 2)]
public class ShopItemVariable : ScriptableObject
{
    public string guid = System.Guid.NewGuid().ToString();

    public Sprite itemSprite;
    public int gemPrice;
}