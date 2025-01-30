using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    public TypeAmmo typeAmmo;
    public override TypeItem GetItemType()
    {
        return TypeItem.Ammo;
    }
}