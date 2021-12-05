using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores any set of items
[CreateAssetMenu(fileName = "GameCreature", menuName = "GameCreature/CreatureTable")]
public class CreatureTable : ScriptableObject
{
    [SerializeField]
    private List<CreatureBase> table;
    public CreatureBase GetCreature(int id)
    {
        return table[id];
    }
    public void AssignCreatureIDs() // Give each item an ID based on its location in the list
    {
        for (int i = 0; i < table.Count; i++)
        {
            try
            {
                table[i].Id = i;
            }
            catch (ItemModifiedException)
            {
                //it's ok
            }
        }
    }

    public List<CreatureBase> GetTable()
    {
        return table;
    }
}
