using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public List<GameObject> itemList;
    public ItemSlotGridDimensioner attachedItemGrid;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<GameObject>();
    }

    public void PlaceItem(ItemInstance item)
    {
        itemList.Add(item.gameObject);
        float tempfloat = item.reference.gridSize.x * item.reference.gridSize.y;

        ItemSlot startSlot = attachedItemGrid.GetItemSlot().GetComponent<ItemSlot>();

        while (!CheckIfFits(item, startSlot))
        {
            int tempInt = startSlot.id + 1;
            if (!attachedItemGrid.GetItemSlot(tempInt))
            {
                
                Debug.Log("not enough room");
                return;
            }

            startSlot = attachedItemGrid.GetItemSlot(tempInt);

        }
        if(!item.containerReference)
        {
            item.containerReference = this;
        }
        startSlot.itemInSlot = item;
        item.parentSlot = startSlot;
        startSlot.ItemCount = item.count;
        startSlot.RefreshInfo();
        //80, -120
        item.PlaceItemInSlot();

        if (tempfloat > 1)
        {
            int x = 1;
            int y = 1;

            ItemSlot currentSlotX = startSlot;
            ItemSlot currentSlotY = startSlot;
            while (y < (int)item.reference.gridSize.y)
            {
                //Debug.Log(y);
                currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = item;
                currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = item.count;
                currentSlotY.RefreshInfo();
                currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                y++;
            }
            while (x < (int)item.reference.gridSize.x)
            {
                y = 1;
                //Debug.Log(x);
                currentSlotX.GetNeighbour(GridDirections.RIGHT).itemInSlot = item;
                currentSlotX.GetNeighbour(GridDirections.RIGHT).ItemCount = item.count;
                currentSlotX.RefreshInfo();
                currentSlotX = currentSlotX.GetNeighbour(GridDirections.RIGHT);
                currentSlotY = currentSlotX;
                while (y < (int)item.reference.gridSize.y)
                {
                    //Debug.Log(y);
                    currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = item;
                    currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = item.count;
                    currentSlotY.RefreshInfo();
                    currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                    y++;
                }
                x++;

            }
            //currentSlot = startSlot;

        }
    }

    public void PlaceItem(ItemInstance item, ItemSlot passedSlot, ItemSlot initalSlot)
    {
        ItemInstance temp = item;
        if(!itemList.Contains(item.gameObject))
            itemList.Add(item.gameObject);
        float tempfloat = item.reference.gridSize.x * item.reference.gridSize.y;

        ItemSlot startSlot = passedSlot;

        while (!CheckIfFits(item, startSlot))
        {
            int tempInt = startSlot.id + 1;
            if (!attachedItemGrid.GetItemSlot(tempInt))
            {

                startSlot = initalSlot;
                item.containerReference = attachedItemGrid.containerReference;
                temp.gameObject.transform.SetParent(startSlot.transform.parent.transform.parent);
                Debug.Log("not enough room");
                break;
            }

            startSlot = attachedItemGrid.GetItemSlot(tempInt);

        }
        if (!item.containerReference)
        {
            item.containerReference = this;
        }
        startSlot.itemInSlot = temp;
        item.parentSlot = startSlot;
        startSlot.ItemCount = item.count;
        startSlot.RefreshInfo();
        //80, -120
        item.PlaceItemInSlot();

        if (tempfloat > 1)
        {
            int x = 1;
            int y = 1;

            ItemSlot currentSlotX = startSlot;
            ItemSlot currentSlotY = startSlot;
            while (y < (int)item.reference.gridSize.y)
            {
                //Debug.Log(y);
                currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = item;
                currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = item.count;
                currentSlotY.RefreshInfo();
                currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                y++;
            }
            while (x < (int)item.reference.gridSize.x)
            {
                y = 1;
                //Debug.Log(x);
                currentSlotX.GetNeighbour(GridDirections.RIGHT).itemInSlot = item;
                currentSlotX.GetNeighbour(GridDirections.RIGHT).ItemCount = item.count;
                currentSlotX.RefreshInfo();
                currentSlotX = currentSlotX.GetNeighbour(GridDirections.RIGHT);
                currentSlotY = currentSlotX;
                while (y < (int)item.reference.gridSize.y)
                {
                    //Debug.Log(y);
                    currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = item;
                    currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = item.count;
                    currentSlotY.RefreshInfo();
                    currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                    y++;
                }
                x++;

            }
            //currentSlot = startSlot;

        }
    }

    public bool CheckIfFits(ItemInstance item, ItemSlot startSlot)
    {
        float tempfloat = item.reference.gridSize.x * item.reference.gridSize.y;
        if (tempfloat > 1)
        {
            int x = 1;
            int y = 1;

            ItemSlot currentSlotX = startSlot;
            ItemSlot currentSlotY = startSlot;

            if (!startSlot || startSlot.itemInSlot)
                return false;

            while (y < (int)item.reference.gridSize.y)
            {
                if (currentSlotY.GetNeighbour(GridDirections.DOWN) && currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot == null)
                {
                    currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                }
                else
                    return false;
                y++;
            }

            while (x < (int)item.reference.gridSize.x)
            {
                y = 1;

                if (currentSlotX.GetNeighbour(GridDirections.RIGHT) && currentSlotX.GetNeighbour(GridDirections.RIGHT).itemInSlot == null)
                {
                    currentSlotX = currentSlotX.GetNeighbour(GridDirections.RIGHT);
                }
                else
                    return false;

                currentSlotY = currentSlotX;
                while (y < (int)item.reference.gridSize.y)
                {
                    if (currentSlotY.GetNeighbour(GridDirections.DOWN) && currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot == null)
                    {
                        currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                    }
                    else
                        return false;
                    y++;
                }
                x++;
            }
            return true;
        }
        else
        {
            if (startSlot && startSlot.itemInSlot == null)
                return true;
            else
                return false;
        }
        
    }

    public void RemoveItem(ItemInstance item)
    {
        attachedItemGrid.ResetIteminSlot(item);

        itemList.Remove(item.gameObject);
    }
}
