using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSessionManager 
{
    private static readonly string DATA_FILE_PATH = Application.persistentDataPath + "/Data.tz";
    public static void SaveData(Inventory inventory, Item[] items, int enemyHealthsPoint, int playerHealthsPoint)
    {
        var saveData = new SaveData(enemyHealthsPoint, playerHealthsPoint, SaveInventory(inventory, items));
        BinaryFormatter bf = new();
        var file = File.Create(DATA_FILE_PATH);
        bf.Serialize(file, saveData);
        file.Close();      
    }
    public static SaveData LoadData()
    {
        if (File.Exists(DATA_FILE_PATH))
        {
            BinaryFormatter bf = new ();
            var file = File.Open(DATA_FILE_PATH, FileMode.Open);
            var saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            return saveData;
        }
        return null;
    }

    private static SlotData[] SaveInventory(Inventory inventory, Item[] items)
    {
        var slots =  inventory.GetAllSlot();
        var slotsData = new List<SlotData>();
        for (int idSlot = 0; idSlot < slots.Length; idSlot++) 
        {
            if (slots[idSlot].IsItem()) 
            {
                for (int idItem = 0; idItem < items.Length-1; idItem++) 
                {                
                    slotsData.Add(new SlotData(idSlot, idItem, slots[idSlot].GetCurrentNumberItem()));
                }
            }
        }
        return slotsData.ToArray();
    }
    
}
