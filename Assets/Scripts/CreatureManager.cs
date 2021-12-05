using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    private static CreatureManager instance;
    public static CreatureManager Instance { get { return instance; } }

    public List<GameObject> creatureList;
    public GameObject prefab;

    [SerializeField] CreatureTable table;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        table.AssignCreatureIDs();
        creatureList = new List<GameObject>();
    }

    public GameObject getCreature()
    {
        foreach (GameObject enemy in creatureList)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        GameObject newItem = Instantiate(prefab);
        newItem.transform.SetParent(transform);
        creatureList.Add(newItem);

        return newItem;
    }

    public BattleUnit CreateCreature()
    {
        GameObject enemyObject = getCreature();
    
        enemyObject.SetActive(true);

        BattleUnit temp = enemyObject.GetComponent<BattleUnit>();
        temp.base_ = table.GetCreature(Random.Range(0, table.GetTable().Count));
        temp.level_ = Random.Range(0, 25);

        return temp;
    }

}
