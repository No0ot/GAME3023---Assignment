using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }

    public ItemInstance itemInstancePrefab;
    public List<GameObject> itemList;

    [SerializeField]
    public ItemTable itemTable;

    private void Awake()
    {
        instance = this;
        itemTable.AssignItemIDs();
        itemList = new List<GameObject>();
    }

    public ItemInstance CreateItemInstance(Item item, int count, Container container)
    {
        
        ItemInstance temp = Instantiate(itemInstancePrefab, this.gameObject.transform.parent.transform);
        temp.Setup(item, count);
        ItemSlot startSlot = container.attachedItemGrid.GetItemSlot();

        if (!startSlot)
        {
            Destroy(temp.gameObject);
            return null;
        }

        while (!container.CheckIfFits(temp, startSlot))
        {
            int tempInt = startSlot.id + 1;
            if (!container.attachedItemGrid.GetItemSlot(tempInt))
            {
                Destroy(temp.gameObject);
                Debug.Log("not enough room");
                return null;
            }

            startSlot = container.attachedItemGrid.GetItemSlot(tempInt);

        }
        itemList.Add(temp.gameObject);
        return temp;
    }
}
