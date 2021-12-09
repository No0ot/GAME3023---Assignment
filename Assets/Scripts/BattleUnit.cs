using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour //The actual creature that the player has, with their level saved
{
    //https://youtu.be/zKRMkD28-xY - Make A Game Like Pokemon in Unity | #7 - Battle System Setup

    [SerializeField] public CreatureBase base_;
    [SerializeField] public int level_;
    [SerializeField] private bool is_player_;

    private Creature battle_creature_;

    public Container equippedRunes;

    public float experience;
    public float experienceNeeded;

    [Header("debug purposes")]
    public float hp;
    public float mp;


    public Creature GetBattleCreature()
    {
        return battle_creature_;
    }

    private void Start()
    {
        MakeNewCreature();
    }

    public void MakeNewCreature()
    {
        battle_creature_ = new Creature(base_, level_);
        experienceNeeded = level_ * 10;
    }

    public void Setup()
    {
        //if (equippedRunes)
        //{
        //    RefreshStats();
        //}

        if (battle_creature_.GetHP() > 0)
        {
            hp = battle_creature_.GetHP();
        }
        else
        {
            battle_creature_.SetHP(1);
            hp = battle_creature_.GetHP();
        }
    }

    public void RefreshStats()
    {
        battle_creature_.ResetStats();

        foreach (GameObject rune in equippedRunes.itemList)
        {
            Item temp = rune.GetComponent<ItemInstance>().reference;
            battle_creature_.AddEquippedStats(temp.addedHP, temp.addedMP, temp.addedAttack, temp.addedDefense, temp.addedSpeed);
        }
    }

    public void KillCreature()
    {
        battle_creature_ = null;
    }

    public void CheatHeal()
    {
        battle_creature_.SetHP(battle_creature_.GetMaxHP());
        battle_creature_.SetMP(battle_creature_.GetMaxMP());
    }

    public void GainExperience(int level)
    {
        experience += level * 5;
        if (experience >= experienceNeeded)
            LevelUP();
    }

    public void LevelUP()
    {
        level_++;
        battle_creature_.LevelUp();
        RefreshStats();
        experience = 0;
        experienceNeeded = level_ * 10;
    }
}
