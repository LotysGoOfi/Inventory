using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Inventory/Armor")]
public class Armor : Item
{
    public int armorValue;
    public TypeArmor typeArmor;
    public override TypeItem GetItemType()
    {
        return TypeItem.Armor;
    }
}
