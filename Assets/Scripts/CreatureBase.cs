using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCreature", menuName = "GameCreature/Create new GameCreature")]
public class CreatureBase : ScriptableObject //Base stats for the creature, without any scaling from the creature's level
{
    //Make A Game Like Pokemon in Unity | #5 - Creating Pokemons Using Scriptable Objects https://youtu.be/x8B_eXfcj6U
    [SerializeField] private string name_;
    [SerializeField] private GameObject sprite_obj_;
    public Sprite portrait;
    [SerializeField] private ElementType elem_type_;

    [SerializeField] private int max_hp_;
    [SerializeField] private int max_mp_;
    [SerializeField] private int attack_;
    [SerializeField] private int defense_;
    [SerializeField] private int speed_;

    [SerializeField] List<CreatureAbility> ability_list_;

    private int id = -1;
    public int Id
    {
        get { return id; }
        set
        {
            id = value;
            throw new ItemModifiedException("Oh no you dont!");
        }
    }

    public string GetName()
    {
        return name_;
    }
    public GameObject GetSpriteObj()
    {
        return sprite_obj_;
    }
    public int GetMaxHP()
    {
        return max_hp_;
    }
    public int GetMaxMP()
    {
        return max_mp_;
    }
    public int GetAttack()
    {
        return attack_;
    }
    public int GetDefense()
    {
        return defense_;
    }
    public int GetSpeed()
    {
        return speed_;
    }
    public List<CreatureAbility> GetAbilityList()
    {
        return ability_list_;
    }
}

[System.Serializable]
public class CreatureAbility
{
    [SerializeField] private AbilityBase ability_base_;
    [SerializeField] private int level_;

    public AbilityBase GetAbilityBase()
    {
        return ability_base_;
    }
    public int GetLevel()
    {
        return level_;
    }
}

public enum ElementType
{
    None,
    Sharp,
    Blunt,
    Light,
    Dark
}