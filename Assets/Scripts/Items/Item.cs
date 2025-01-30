using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject , IInventoryItem
{
    [SerializeField]
    private string ItemName;

    [SerializeField]
    private Sprite Icon;

    [SerializeField]
    private float Weight;

    [SerializeField]
    private int MaxNumber;

 
    public string GetName() => ItemName;


    public int GetMexNumber() => MaxNumber;


    public Sprite GetIcon() => Icon;

    public virtual TypeItem GetItemType() => TypeItem.Default;

    public virtual float GetWeight() => Weight;
    
}

