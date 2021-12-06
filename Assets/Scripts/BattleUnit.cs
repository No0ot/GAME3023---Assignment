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
    }

    public void Setup()
    {
        if (equippedRunes)
        {
            foreach (GameObject rune in equippedRunes.itemList)
            {
                Item temp = rune.GetComponent<ItemInstance>().reference;
                battle_creature_.AddEquippedStats(temp.addedHP, temp.addedMP, temp.addedAttack, temp.addedDefense, temp.addedSpeed);
            }
        }
        hp = battle_creature_.GetHP();
    }

    public void KillCreature()
    {
        battle_creature_ = null;
    }
}
