using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItems : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private IInventoryItem draggedItem;
    private int currentSlotIndex;
    private int currentNumberItem;
    private Image draggedItemImage;
    private RectTransform inventoryRectTransform;
    private Inventory inventory;

    private void Awake()
    {
        draggedItemImage = CreateDraggedItemImage();
        inventoryRectTransform = GetComponent<RectTransform>();
        inventory = GetComponent<Inventory>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentSlotIndex = GetCellIndexUnderMouse(eventData);
        if (IsDraggingValid(currentSlotIndex)) 
        {
            StartDragging(currentSlotIndex,eventData);
        }     
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveDraggedItem(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int targetCellIndex = GetCellIndexUnderMouse(eventData);
        HandleDrop(targetCellIndex);
    }

    private void StartDragging(int slotIndex, PointerEventData eventData)
    {
        var slot = inventory.GetSlot(slotIndex); 
        draggedItem = slot.GetItem();
        currentNumberItem = slot.GetCurrentNumberItem(); 
        inventory.DeliteItem(slotIndex);
        UpdateDraggedItemImage(eventData);
    }
    private void UpdateDraggedItemImage(PointerEventData eventData)
    {
        MoveDraggedItem(eventData.position);
        draggedItemImage.sprite = draggedItem.GetIcon();
        draggedItemImage.gameObject.SetActive(true);
    }
    private void MoveDraggedItem(Vector3 mousePosition)
    {
        if (draggedItem != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(inventoryRectTransform, mousePosition, null, out var localPoint);
            draggedItemImage.rectTransform.anchoredPosition = localPoint;
        }
    }
    private void HandleDrop(int targetSlotIndex)
    {
        if (draggedItem == null) return;
        if (IsDraggingValid(targetSlotIndex))
        {
            (IInventoryItem remainderItem, int numberItem) = inventory.AddItem(targetSlotIndex, draggedItem, currentNumberItem);
            if (remainderItem == null)
            {
                CleanupDraggedItem();
                return;
            }
            ReturnItemToSource(remainderItem,numberItem);
            return;
        }
        if (inventory.IsValidSlotIndex(targetSlotIndex)){

            (IInventoryItem remainderItem, int numberItem) = inventory.AddItem(targetSlotIndex, draggedItem,currentNumberItem);
            if (remainderItem == null)
            {
                CleanupDraggedItem();
                return;
            }
            ReturnItemToSource(remainderItem, numberItem);
            CleanupDraggedItem();
            return;
        }
        ReturnItemToSource(draggedItem,currentNumberItem);
    }

    private int GetCellIndexUnderMouse(PointerEventData eventData)
    {
        for (int i = 0; i < inventory.GetMaxIndexSlot(); i++)
        {
            if (IsPointerOverSlot(eventData.position, inventory.GetSlotRectTransform(i)))
            {
                return i;
            }
        }
        return -1;
    }
    private void ReturnItemToSource(IInventoryItem item,int  newNumberItem)
    {
        inventory.AddItem( currentSlotIndex, item, newNumberItem);
        CleanupDraggedItem();
    }
    private void CleanupDraggedItem()
    {
        draggedItem = null;
        draggedItemImage.gameObject.SetActive(false);
        currentNumberItem = 0;
    }

    private bool IsPointerOverSlot(Vector2 pointerPosition, RectTransform rectTransformSlot)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransformSlot, pointerPosition);
    }

    private bool IsDraggingValid(int SlotIndex)
    {
        return inventory.IsItemSlot(SlotIndex);
    }


    private Image CreateDraggedItemImage()
    {
        var image = new GameObject("DraggedItem").AddComponent<Image>();
        image.transform.SetParent(transform.root, false);
        image.raycastTarget = false;
        image.gameObject.SetActive(false);
        return image;
    }

}
