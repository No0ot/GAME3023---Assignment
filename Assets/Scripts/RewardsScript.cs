using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsScript : MonoBehaviour
{
    [SerializeField] Container rewardContainer;

    private void Start()
    {
        Invoke("DelayStart", 0.2f);
       
    }

    public void GenerateRewards(int level)
    {
        if(!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        int numRunes;

        switch(level)
        {
            case int temp when level <= 10:
                numRunes = Random.Range(0, 2);
                break;
            case int temp when level <= 15:
                numRunes = Random.Range(1, 3);
                break;
            case int temp when level <= 20:
                numRunes = Random.Range(1, 4);
                break;
            case int temp when level > 20:
                numRunes = Random.Range(2, 4);
                break;
            default:
                numRunes = Random.Range(0, 2);
                break;
        }
        GenerateRunes(numRunes);
    }

    private void GenerateRunes(int num)
    {
       // Debug.Log(num);
        if (num == 0)
            return;
        for (int i = 0; i <= num; i++)
        {
            int runeID;
            int runeQuality = Random.Range(1, 101);
            Debug.Log("Rune Roll: " + runeQuality);
            if (runeQuality < 60)
                runeID = Random.Range(0, 8);
            else if (runeQuality < 91)
                runeID = Random.Range(8, 16);
            else
                runeID = Random.Range(16, 24);

            Debug.Log("Rune ID: " + runeID);
            ItemInstance temp = ItemManager.Instance.CreateItemInstance(ItemManager.Instance.itemTable.GetItem(runeID), 1, rewardContainer);
            if (temp != null)
                rewardContainer.PlaceItem(temp);
        }
    }

    public void CloseButtonClicked()
    {
        rewardContainer.ClearContainer();
        gameObject.SetActive(false);
    }

    void DelayStart()
    {
        GenerateRewards(16);
        CloseButtonClicked();
    }

}
