using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject containerCanvas;

    public Container inventoryContainer;

    private void Start()
    {
        //AddItem(3, 5);
        //AddItem(0, 1);
        //AddItem(0, 1);
        //AddItem(0, 1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddItem(Random.Range(0, ItemManager.Instance.itemTable.GetTable().Count), 1) ;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddItem(0, 5);
            AddItem(0, 5);
            AddItem(0, 5);
            AddItem(0, 5);
            AddItem(0, ItemManager.Instance.itemTable.GetItem(0).maxCount);
        }
    }

    public void OpenContainer()
    {
        containerCanvas.SetActive(true);
        
    }

    public void CloseContainer()
    {
        containerCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ContainerTrigger")
        {
            containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().containerReference = collision.transform.parent.GetComponent<Container>();
            foreach (GameObject item in containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().containerReference.itemList)
            {
                item.SetActive(true);
                //item.GetComponent<ItemInstance>().parentSlot.itemInSlot = item.GetComponent<ItemInstance>();
                containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().containerReference.PlaceItem(item.GetComponent<ItemInstance>(), item.GetComponent<ItemInstance>().parentSlot, item.GetComponent<ItemInstance>().parentSlot);
            }
            OpenContainer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ContainerTrigger")
        {
            CloseContainer();
            foreach(GameObject item in containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().containerReference.itemList)
            {
                item.SetActive(false);
                //item.GetComponent<ItemInstance>().parentSlot.itemInSlot = null;
                containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().ResetIteminSlot(item.GetComponent<ItemInstance>());
            }
            containerCanvas.transform.GetChild(2).GetComponent<ItemSlotGridDimensioner>().containerReference = null;
        }
    }

    public void AddItem(int id, int count)
    {
        ItemInstance temp = ItemManager.Instance.CreateItemInstance(ItemManager.Instance.itemTable.GetItem(id), count, inventoryContainer);
        if(temp)
            inventoryContainer.PlaceItem(temp);
    }

    public void AddItem(int id)
    {
        ItemInstance temp = ItemManager.Instance.CreateItemInstance(ItemManager.Instance.itemTable.GetItem(id), 1, inventoryContainer);
        if (temp)
            inventoryContainer.PlaceItem(temp);
    }
}
