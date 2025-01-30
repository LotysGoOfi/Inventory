using System;

[Serializable]
public class SaveData
{
    public int enemyHealthsPoint;
    public int playerHealthsPoint;
    public SlotData[] slots;

    public SaveData(int enemyHealthsPoint, int playerHealthsPoint, SlotData[] slots)
    {
        this.enemyHealthsPoint = enemyHealthsPoint;
        this.playerHealthsPoint = playerHealthsPoint;
        this.slots = slots;
    }
}
[Serializable]
public class SlotData
{
    public int slotID;
    public int itemID;
    public int numberItem;

    public SlotData(int slotID, int itemID, int numberItem)
    {
        this.slotID = slotID;
        this.itemID = itemID;
        this.numberItem = numberItem;
    }
}

