using UnityEngine.Events;

public class HeadSlot : Slot
{
    public UnityEvent<int> updateArnorHead = new();

    public override (IInventoryItem, int) AddItem(IInventoryItem newItem, int newCoutItem)
    {
        if (newItem is Armor armor)
        {      
            if (armor.typeArmor == TypeArmor.Head)
            {
                if (IsItem())
                {
                    var temporaryItem = item;
                    var temporaryCountItem = newCoutItem;
                    SetItem(newItem, armor.armorValue);
                    updateArnorHead.Invoke(armor.armorValue);
                    return (temporaryItem, temporaryCountItem);
                }
                SetItem(newItem, armor.armorValue);
                updateArnorHead.Invoke(armor.armorValue);
                return (null, 0);
            }
        }
        return (newItem, newCoutItem);
    }

    public override int GetCurrentNumberItem() => 1;
    public override void RemoveItem()
    {
        item = null;
        numberText.text = "";
        icon.sprite = null;
        currentCountItems = 0;
        updateArnorHead.Invoke(0);
    }
}
