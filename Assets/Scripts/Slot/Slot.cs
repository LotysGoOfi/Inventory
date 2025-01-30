using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public static UnityEvent<Slot> ItemClicked = new();
   
    [SerializeField]
    protected Image icon;

    [SerializeField]
    protected Text numberText;

    [SerializeField]
    protected IInventoryItem item;

    [SerializeField]
    protected int currentCountItems = 0;

    private RectTransform rectTransform;

    public RectTransform RectTransform { get => rectTransform;}

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public virtual int GetCurrentNumberItem()
    {
        return currentCountItems;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsItem())
        {
            ItemClicked.Invoke(this);          
        }
        return;
    }

    public IInventoryItem GetItem() => item;
    

    public virtual (IInventoryItem, int) AddItem(IInventoryItem newItem, int newCoutItem)
    {
        
        if (IsItem())
        {
            if ((item.GetMexNumber() > 1) && item.Equals(newItem)){

                var remainder = AddItemNumber(newCoutItem);
                SetNumber(currentCountItems);
                if (remainder<=0)
                {                 
                    return (null,0);
                }
                else
                {
                    return (newItem,remainder);
                }
            }
            return (newItem,newCoutItem);
        }
        else
        {
            SetItem(newItem,newCoutItem);
            return (null,0);
        }
    }

    public void RemoveItemNumber(int number)
    {
        if (currentCountItems <= number)
        {
            RemoveItem();
            return;
        }
        currentCountItems -= number;
        SetNumber(currentCountItems);
    }
    public int AddItemNumber(int number)
    {
        var sumNumber = currentCountItems + number;
        var maxNumber = item.GetMexNumber();

        if (maxNumber >= sumNumber)
        {
            currentCountItems = sumNumber;
            return 0;
        }
        var remainder = sumNumber - maxNumber;
        currentCountItems = maxNumber;
        return remainder;
    }

    public virtual void SetItem(IInventoryItem newItem,int number)
    {
        currentCountItems = number;
        icon.sprite = newItem.GetIcon();
        SetNumber(number);
        item = newItem;
    }

    public virtual void SetNumber(int number)
    {
        if (number > 1)
        {
            numberText.text = number + "";
        }
        else
        {
            numberText.text = "";
        }
    }

    public virtual void RemoveItem() 
    { 
        item = null;
        numberText.text = "";
        icon.sprite = null;
        currentCountItems = 0;
    }

    public bool IsItem()
    {
        return item != null;
    }
}
