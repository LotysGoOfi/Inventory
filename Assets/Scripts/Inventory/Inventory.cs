using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Slot[] slots;


    public Slot[] GetAllSlot() => slots;
    public List<Slot> GetSlotsIsItem()
    {
        var slotIsItem = new List<Slot>();
        foreach (Slot slot in slots)
        {
            if(slot.IsItem()) slotIsItem.Add(slot);
        }
        return slotIsItem;
    }
    public Slot GetEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.IsItem())
            {
                return slot;
            }
        }
        return null;
    }

    public void DeliteAllItems()
    {
        foreach (Slot slot in slots)
        {
            slot.RemoveItem();
        }
    }
    public Slot GetSlotAmmoPistol()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsItem())
            {
                if (slot.GetItem() is Ammo ammo)
                {
                   
                    if (ammo.typeAmmo == TypeAmmo.AmmunitionPistol)
                    {
                        return slot;
                    }
                }
            }
        }
        return null;
    }
    public Slot GetSlotAmmoGun()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsItem())
            {
                if (slot.GetItem() is Ammo ammo)
                {                  
                    if (ammo.typeAmmo == TypeAmmo.AmmunitionMachineGun)
                    {
                        return slot;
                    }
                }
            }
        }
        return null;
    }


    public BodySlot GetBodySlot()
    {
        foreach (Slot slot in slots)
        {
            if(slot is BodySlot bodySlot)
            return bodySlot;
        }
        return null;
    }

    public HeadSlot GetHeadSlot()
    {
        foreach (Slot slot in slots)
        {
           if (slot is HeadSlot headSlot)
           return headSlot;
        }
        return null;
    }

    public int GetMaxIndexSlot() => slots.Length;

    public RectTransform GetSlotRectTransform(int slotId) => slots[slotId].RectTransform;
    public Slot GetSlot(int  slotId) => slots[slotId];
     
    public bool IsValidSlotIndex(int index) => index >= 0 && index < slots.Length;

    public bool IsItemSlot(int index) => IsValidSlotIndex(index) && slots[index].IsItem();

    public IInventoryItem GetItem(int index) => slots[index].GetItem();

    public void DeliteItem(int index) => slots[index].RemoveItem();
    public (IInventoryItem,int ) AddItem(int idSlot,IInventoryItem item,int number) => slots[idSlot].AddItem(item,number);
    

}
