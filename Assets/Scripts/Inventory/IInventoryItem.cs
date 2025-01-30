using UnityEngine;

public interface IInventoryItem 
{
    public string GetName();
    public TypeItem GetItemType();
    public int GetMexNumber();
    public float GetWeight();
    public Sprite GetIcon();

}
