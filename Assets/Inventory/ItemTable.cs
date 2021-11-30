using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores any set of items
[CreateAssetMenu(fileName = "NewItemTable", menuName = "ItemSystem/ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    private List<Item> table;  //The index of each item in the table is its ID
    public Item GetItem(int id)
    {
        return table[id];
    }
    public void AssignItemIDs() // Give each item an ID based on its location in the list
    {
        for(int i = 0; i < table.Count; i++)
        {
            try
            {
                table[i].Id = i;
            } catch(ItemModifiedException)
            {
                //it's ok
            }
        }
    }

    public List<Item> GetTable()
    {
        return table;
    }
}
