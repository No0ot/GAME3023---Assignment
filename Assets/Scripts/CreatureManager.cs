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

    private void Awake()
    {
        instance = this;
        table.AssignCreatureIDs();
        creatureList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        int tempint = Random.Range(0, table.GetTable().Count);
        Debug.Log(tempint);
        temp.base_ = table.GetCreature(tempint);
        temp.level_ = Random.Range(0, 25);
        temp.MakeNewCreature();

        return temp;
    }

}
