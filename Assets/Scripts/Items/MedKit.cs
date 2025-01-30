using UnityEngine;

[CreateAssetMenu(fileName = "NewMedKit", menuName = "Inventory/MedKit")]
public class MedKit : Item
{
    public int healAmount;
    public override TypeItem GetItemType()
    {
        return TypeItem.Medicament;
    }
}
