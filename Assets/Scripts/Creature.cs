using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    //Make A Game Like Pokemon in Unity | #5 - Creating Pokemons Using Scriptable Objects https://youtu.be/x8B_eXfcj6U
    private CreatureBase base_;
    private int level_;
    private float maxHP_;
    private float maxMP_;
    private float hp_;
    private float mp_;
    private int spd_;
    private int atk_;
    private int def_;

    public List<Ability> abilities;

    public Creature(CreatureBase creature_base, int level)
    {
        base_ = creature_base;
        level_ = level;
        maxHP_ = GetMaxHPFromBase();
        maxMP_ = GetMaxMPFromBase();
        spd_ = GetBaseSpeed();
        atk_ = GetBaseAttack();
        def_ = GetBaseDefense();
        hp_ = maxHP_;
        mp_ = maxMP_;

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
    public int GetBaseAttack()
    {
        return Mathf.FloorToInt((base_.GetAttack() * level_) / 100f) + 5; //Pokemon formula
    }
    public int GetBaseDefense()
    {
        return Mathf.FloorToInt((base_.GetDefense() * level_) / 100f) + 5; //Pokemon formula
    }
    public int GetBaseSpeed()
    {
        return Mathf.FloorToInt((base_.GetSpeed() * level_) / 100f) + 5; //Pokemon formula
    }

    public int GetAttack()
    {
        return atk_;
    }
    public int GetDefense()
    {
        return def_;
    }
    public int GetSpeed()
    {
        return spd_;
    }
    public int GetMaxHPFromBase()
    {
        return Mathf.FloorToInt((base_.GetMaxHP() * level_) / 100f) + 10; //Pokemon formula
    }
    public int GetMaxMPFromBase()
    {
        return Mathf.FloorToInt((base_.GetMaxMP() * level_) / 100f) + 10; //NOT Pokemon formula
    }

    public float GetMaxHP()
    {
        return maxHP_;
    }
    public float GetMaxMP()
    {
        return maxMP_;
    }
    public float GetHP()
    {
        return hp_;
    }
    public void SetHP(float value)
    {
        hp_ = (value > maxHP_) ? maxHP_ : value;
    }
    public float GetMP()
    {
        return mp_;
    }
    public void SetMP(float value)
    {
        mp_ = (value > maxMP_) ? maxMP_ : value;
    }
    public List<Ability> GetAbilities()
    { 
        return abilities;
    }

    public Sprite GetPortraitSprite()
    {
        return base_.portrait;
    }

    public bool CanSpendMP(Ability ability)
    {
        if (GetMP() >= ability.GetBase().GetMPCost())
        {
            return true; //enough MP
        }
        return false; //not enough MP
    }

    public bool SpendMP(Ability ability)
    {
        if (CanSpendMP(ability))
        {
            SetMP(GetMP() - ability.GetBase().GetMPCost());
            return true; //used MP
        }
        return false; //not enough MP
    }

    public bool DealDamage(Ability ability)
    {
        // POKEMON FORMULA
        float mod = Random.Range(0.85f, 1f);
        float atk = (2 * GetLevel() + 10) / 250f;
        float def = atk * ability.GetBase().GetPower() * ((float)GetAttack() / GetDefense()) + 2;
        int damage = Mathf.FloorToInt(def * mod);
        AbilityType type = ability.GetBase().GetAbilityType();
        switch (type)
        {
            case AbilityType.Normal:
                break;
            case AbilityType.HealHp:
                float heal_amount = (float)ability.GetBase().GetPower() / (float)GetMaxHP() * 100.0f;
                SetHP(GetHP() + heal_amount);
                break;
            case AbilityType.DrainMp:
                break;
            case AbilityType.OneShot:
                break;
        }
        return false;
    }

    public DamageResult TakeDamage(Ability ability, Creature attacker)
    {
        if (UnityEngine.Random.Range(1,101) > ability.GetBase().GetAccuracy())
        {
            return DamageResult.MissedDamage;
        }
        // POKEMON FORMULA
        float mod = Random.Range(0.85f, 1f);
        float atk = (2 * attacker.GetLevel() + 10) / 250f;
        float def = atk * ability.GetBase().GetPower() * ((float)attacker.GetAttack() / GetDefense()) + 2;
        float damage = Mathf.FloorToInt(def * mod);
        AbilityType type = ability.GetBase().GetAbilityType();
        switch (type)
        {
            case AbilityType.Normal:
                SetHP(GetHP() - damage);
                if (GetHP() <= 0)
                {
                    SetHP(0);
                    return DamageResult.Death; //death
                }
                break;
            case AbilityType.HealHp:
                return DamageResult.NoDamage;
                break;
            case AbilityType.DrainMp:
                float pre_mp = GetMP();
                SetMP(GetMP() - ability.GetBase().GetPower());
                if (GetMP() <= 0)
                {
                    SetMP(0);
                }
                float post_mp = GetMP();
                float attacker_gained_mp = pre_mp - post_mp;
                attacker.SetMP(attacker.GetMP() + attacker_gained_mp);
                break;
            case AbilityType.OneShot:
                SetHP(GetHP() - damage);
                if (GetHP() <= 0)
                {
                    SetHP(0);
                    return DamageResult.Death; //death
                }
                break;
        }
        return DamageResult.TookDamage; //survives
    }

    public Ability GetRandAbility()
    {
        List<int> viable_idx = new List<int>();
        bool is_viable = false;
        int loop_count = 0; //emergency exit
        for (int i = 0; i < GetAbilities().Count; i++)
        {
            viable_idx.Add(i);
        }
        while (viable_idx.Count > 0 || is_viable == false || loop_count < 50)
        {
            Debug.Log(">>> Choosing ability...");
            int r = Random.Range(0, viable_idx.Count);
            if (CanSpendMP(GetAbilities()[r]))
            {
                is_viable = true;
                return GetAbilities()[r];
            }
            else
            {
                viable_idx.RemoveAt(r);
            }
            loop_count++;
        }
        return null;
    }

    public void ResetStats()
    {
        maxHP_ = GetMaxHPFromBase();
        maxMP_ = GetMaxMPFromBase();
        spd_ = GetBaseSpeed();
        atk_ = GetBaseAttack();
        def_ = GetBaseDefense();
        if (hp_ > maxHP_)
            hp_ = maxHP_;
        if (mp_ > maxMP_)
            mp_ = maxMP_;
    }

    public void AddEquippedStats(int hp, int mp, int atk, int def, int spd)
    {
        maxHP_ += hp;
        maxMP_ += mp;
        hp_ += hp;
        mp_ += mp;
        atk_ += atk;
        def_ += def;
        spd_ += spd;
    }

    public void LevelUp()
    {
        level_++;
    }
}

public enum DamageResult
{
    NoDamage,
    TookDamage,
    Death,
    MissedDamage
}