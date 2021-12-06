//
// Credits to https://www.youtube.com/watch?v=BGr-7GZJNXg for the drag and drop functionality
// Credits to https://catlikecoding.com/unity/tutorials/hex-map/ I used this a while ago for a project with a hex grid but i still use the method of making a grid just
// changed to a square grid.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum GridDirections
{
    UP,
    LEFT,
    DOWN,
    RIGHT
}
// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public ItemSlotGridDimensioner gridReference;
    public ItemInstance itemInSlot = null;
    public Vector2 gridCoordinate;
    public ItemSlot[] neighbours;
    public int id;

    [SerializeField]
    private int itemCount = 0;
    public int ItemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
            itemCount = value;
        }
    }

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    void Start()
    {
        RefreshInfo();
    }

    public void UseItemInSlot()
    {
        if(itemInSlot != null)
        {
            itemInSlot.reference.Use();
            if (itemInSlot.reference.isConsumable)
            {
                itemCount--;
                RefreshInfo();
            }
        }
    }

    public void RefreshInfo()
    {
        if(ItemCount < 1)
        {
            itemInSlot = null;
        }

        if(itemInSlot != null) // If an item is present
        {
            //update image and text
            itemCountText.text = ItemCount.ToString();
            //icon.sprite = itemInSlot.reference.icon;
            //icon.gameObject.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        } else
        {
            // No item
            itemCountText.text = "";
            //icon.gameObject.SetActive(false);
        }
    }

    public ItemSlot GetNeighbour(GridDirections direction)
    {
        return neighbours[(int)direction];
    }

    public void SetNeighbour(GridDirections direction, ItemSlot slot)
    {
        neighbours[(int)direction] = slot;
        slot.neighbours[(int)GetOppositeNeighbour(direction)] = this;
    }

    public GridDirections GetOppositeNeighbour(GridDirections direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropped");
        if (eventData.pointerDrag != null)
        {
            ItemInstance temp = eventData.pointerDrag.GetComponent<ItemInstance>();
            //temp.gameObject.transform.SetParent(this.transform.parent.transform.parent);
            temp.containerReference.RemoveItem(temp);
            //temp.parentSlot.itemInSlot = null;
            ItemSlot tempslot = temp.parentSlot;
            temp.parentSlot = this;
            temp.containerReference = temp.parentSlot.gridReference.containerReference;
            temp.containerReference.PlaceItem(temp, temp.parentSlot, tempslot);
            temp.PlaceItemInSlot();
            //gridReference.
        }
    }
}
