using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    //Make A Game Like Pokemon in Unity | #5 - Creating Pokemons Using Scriptable Objects https://youtu.be/x8B_eXfcj6U
    private CreatureBase base_;
    private int level_;
    private int hp_;
    private int mp_;

    public List<Ability> abilities;

    public Creature(CreatureBase creature_base, int level)
    {
        base_ = creature_base;
        level_ = level;
        hp_ = GetMaxHP();
        mp_ = GetMaxMP();

        abilities = new List<Ability>();
        foreach (var move in creature_base.GetAbilityList())
        {
            if (move.GetLevel() <= level_)
            {
                abilities.Add(new Ability(move.GetAbilityBase()));
            }
        }
    }

    public CreatureBase GetBaseStats()
    {
        return base_;
    }
    public int GetLevel()
    {
        return level_;
    }
    public int GetAttack()
    {
        return Mathf.FloorToInt((base_.GetAttack() * level_) / 100f) + 5; //Pokemon formula
    }
    public int GetDefense()
    {
        return Mathf.FloorToInt((base_.GetDefense() * level_) / 100f) + 5; //Pokemon formula
    }
    public int GetSpeed()
    {
        return Mathf.FloorToInt((base_.GetSpeed() * level_) / 100f) + 5; //Pokemon formula
    }
    public int GetMaxHP()
    {
        return Mathf.FloorToInt((base_.GetMaxHP() * level_) / 100f) + 10; //Pokemon formula
    }
    public int GetMaxMP()
    {
        return Mathf.FloorToInt((base_.GetMaxMP() * level_) / 100f); //NOT Pokemon formula
    }
    public int GetHP()
    {
        return hp_;
    }
    public void SetHP(int value)
    {
        hp_ = value;
    }
    public int GetMP()
    {
        return mp_;
    }
    public void SetMP(int value)
    {
        mp_ = value;
    }
    public List<Ability> GetAbilities()
    { 
        return abilities;
    } 
}
