//
// Credits to https://www.youtube.com/watch?v=BGr-7GZJNXg for the drag and drop functionality
//
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemInstance : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Item reference;

    public Vector2 size;

    public Image icon;
    public TMP_Text countText;
    public int count;

    public ItemSlot parentSlot;

    public Canvas canvas;
    private RectTransform rectTransform;
    CanvasGroup canvasGroup;

    public Container containerReference;

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(Item item, int thiscount)
    {
        reference = item;
        count = thiscount;
        icon.sprite = reference.icon;
        if (item.isConsumable)
            transform.GetChild(1).GetComponent<TMP_Text>().text = count.ToString();
        else
            transform.GetChild(1).gameObject.SetActive(false);
        icon.rectTransform.sizeDelta = new Vector2(item.gridSize.x * 96, item.gridSize.y * 96);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("drag begin");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("draag");
        icon.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("drag end");
        canvasGroup.blocksRaycasts = true;
        PlaceItemInSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ///eventData.pointerDrag.GetComponent<ItemInstance>().PlaceItemInSlot();
            if (eventData.pointerDrag.GetComponent<ItemInstance>().reference == reference && reference.isConsumable && count < reference.maxCount)
            {
                count += eventData.pointerDrag.GetComponent<ItemInstance>().count;
                countText.SetText(count.ToString());
                Destroy(eventData.pointerDrag.gameObject);
                return;
            }

        }
    }

    public void PlaceItemInSlot()
    {
        rectTransform.position = parentSlot.transform.GetComponent<RectTransform>().position;

        //Debug.Log(parentSlot.transform.GetComponent<RectTransform>().anchoredPosition);

    }
}
