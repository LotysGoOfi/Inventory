using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Slot currentSlot;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Image iconItem;

    [SerializeField]
    private Button buttonItem;

    [SerializeField]
    private Text buttonItemText;

    [SerializeField]
    private Text textNameItem;

    [SerializeField]
    private bool active = false;

    [SerializeField]
    private BodySlot bodySlot;

    [SerializeField]
    private HeadSlot headSlot;

    [SerializeField]
    private Text armorText;
    
    [SerializeField]
    private GameObject iconArmorObject;

    [SerializeField]
    private Text weightText;

    [SerializeField]
    private PlayerController playerController;
 

    void Start()
    {
        Slot.ItemClicked.AddListener(ActivePanel);
    }  

    public void ActivePanel(Slot slot)
    {
        currentSlot = slot;
        iconItem.sprite = slot.GetItem().GetIcon();
        active = true;
        panel.SetActive(true);
        ActiveItemPanel(slot.GetItem());
    }

    public void ButtonClickDelete()
    {
        currentSlot.RemoveItem();
        ClosePanel();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active) 
        {
            if(!RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(), eventData.position))
            {
                ClosePanel();
            }
        }
    }

    public void ClosePanel()
    {
        iconItem.sprite = null;
        active = false;
        panel.SetActive(false);
        buttonItem.onClick.RemoveAllListeners();
        iconArmorObject.SetActive(false);
    }

    public void ActiveItemPanel(IInventoryItem item)
    {
        weightText.text = item.GetWeight()+"";
        
        if (item is Armor armor) 
        {
            armorText.text = armor.armorValue+"";
            iconArmorObject.SetActive(true);
            buttonItemText.text = "Equip";
            buttonItem.onClick.AddListener(()=>SetButenItemArrmor(armor));
            return; 
        }
        if (item is Ammo ammo) 
        { 
            buttonItemText.text = "Buy cartridges";
            buttonItem.onClick.AddListener(() => SetButenItemAmmo(ammo));
            return; 
        }
        if (item is MedKit medKit) { 
            buttonItemText.text = "Heal";
            buttonItem.onClick.AddListener(() => SetButenItemMedik(medKit));
            return; 
        }
        buttonItemText.text = "Defoult";
    }

    private void SetButenItemArrmor(Armor armor) 
    {
        IInventoryItem currentItem = null;

        if(armor.typeArmor == TypeArmor.Head) 
        {
            (currentItem, _) = headSlot.AddItem(armor, 1);
            currentSlot.RemoveItem();
        }
        if(armor.typeArmor == TypeArmor.Body) 
        {
            (currentItem,_)= bodySlot.AddItem(armor, 1);
            currentSlot.RemoveItem();
        }
        if(currentItem == null)
        {
           
            ClosePanel();
            return;
        }
        currentSlot.AddItem(currentItem, 1);
        
        ClosePanel();
    }

    private void SetButenItemAmmo(Ammo ammo) 
    { 
        currentSlot.AddItem(ammo, ammo.GetMexNumber());
        ClosePanel();
    }

    private void SetButenItemMedik(MedKit medKit) 
    {
        playerController.Heal(medKit.healAmount);
        currentSlot.RemoveItemNumber(1);
        ClosePanel();
    }
    
}
