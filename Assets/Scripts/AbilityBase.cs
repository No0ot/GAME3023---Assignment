using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCreature", menuName = "GameCreature/Create new Ability")]
public class AbilityBase : ScriptableObject
{
    //Make A Game Like Pokemon in Unity | #6 - Creating Moves For Pokemon https://youtu.be/h3EZ9G0Gtx0
    [SerializeField] private string name_;

    [SerializeField] private ElementType elem_type_;

    [SerializeField] private int power_;
    [SerializeField] private int accuracy_;
    [SerializeField] private int mp_cost_;
    [SerializeField] private AbilityType ability_type_;
    [SerializeField] private AnimType anim_type_;

    public string GetName()
    {
        return name_;
    }
    public ElementType GetElem()
    {
        return elem_type_;
    }
    public int GetPower()
    {
        return power_;
    }
    public int GetAccuracy()
    {
        return accuracy_;
    }
    public int GetMPCost()
    {
        return mp_cost_;
    }
    public AbilityType GetAbilityType()
    {
        return ability_type_;
    }
    public AnimType GetAnimType()
    {
        return anim_type_;
    }
}

public enum AbilityType
{
    Normal,
    HealHp,
    DrainMp,
    OneShot
}

public enum AnimType
{
    Default,
    Bite,
    Punch,
    Guard,
    Slash,
    Projectile
}