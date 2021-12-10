using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    //https://youtu.be/zKRMkD28-xY - Make A Game Like Pokemon in Unity | #7 - Battle System Setup
    //https://youtu.be/SXBgBmUcTe0 - Using Menus with A Controller/Keyboard in Unity
    //https://youtu.be/joJTzYM72Dg - Make A Game Like Pokemon in Unity | #8 - Battle System - Action and Move Selection

    [SerializeField] private BattleHudController hud_;
    [SerializeField] public BattleUnit player_unit_;
    [SerializeField] public BattleUnit enemy_unit_;
    [SerializeField] private GameObject first_selected_button_;

    [SerializeField] public GameObject playerPos;
    [SerializeField] public GameObject enemyPos;
    [SerializeField] RewardsScript rewardSystem;
    private BattleState state_;

    public bool battleStarted;

    public event Action<bool> OnBattleOver;
    //private void OnEnable()
    //{
    //    SetupBattle();
    //}
    //void Start()
    //{
    //    SetupBattle();
    //}

    public void DoUpdate()
    {
        switch (state_)
        {
            case BattleState.kStart:
                break;
            case BattleState.kPlayerAction:
                break;
            case BattleState.kPlayerAbility:
                break;
            case BattleState.kEnemyTurn:
                break;
            case BattleState.kBusy:
                break;
            default:
                break;
        }
    }

    public void CreateBattle()
    {
        if(!battleStarted)
        {
            SetupBattle();
            battleStarted = true;
        }
    }

    public void CreateBattle(BattleUnit boss)
    {
        if (!battleStarted)
        {
            SetupBattle(boss);
            battleStarted = true;
        }
    }

    public void SetupBattle()
    {
        enemy_unit_ = CreatureManager.Instance.CreateCreature();
        player_unit_.Setup();
        enemy_unit_.Setup();
        player_unit_.gameObject.transform.position = playerPos.transform.position;
        enemy_unit_.gameObject.transform.position = enemyPos.transform.position;

        hud_.SetPlayerData(player_unit_.GetBattleCreature());
        hud_.SetEnemyData(enemy_unit_.GetBattleCreature());

        hud_.ClearCombatLog();
        hud_.UpdateCombatLog("You have encountered an enemy " + enemy_unit_.base_.GetName());

        hud_.SetAbilityNames(player_unit_.GetBattleCreature().GetAbilities());
        hud_.SetActiveActionList(true);
        hud_.SetActiveAbilityList(false);

        // SPEED CHECK
        //DoPlayerAction();
        if (player_unit_.GetBattleCreature().GetSpeed() >= enemy_unit_.GetBattleCreature().GetSpeed())
        {
            hud_.UpdateCombatLog("Player gets 1st turn.");
            DoPlayerAction();
        }
        else
        {
            hud_.UpdateCombatLog("Enemy gets 1st turn!");
            StartCoroutine(DoEnemyTurn());
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(first_selected_button_);
    }

    public void SetupBattle(BattleUnit boss)
    {
        boss.GetComponent<SpriteRenderer>().enabled = false;
        enemy_unit_ = boss;
        player_unit_.Setup();
        enemy_unit_.Setup();

        player_unit_.gameObject.transform.position = playerPos.transform.position;
        enemy_unit_.gameObject.transform.position = enemyPos.transform.position;

        hud_.SetPlayerData(player_unit_.GetBattleCreature());
        hud_.SetEnemyData(enemy_unit_.GetBattleCreature());

        hud_.ClearCombatLog();
        hud_.UpdateCombatLog("You have encountered a Boss enemy " + enemy_unit_.base_.GetName());

        hud_.SetAbilityNames(player_unit_.GetBattleCreature().GetAbilities());
        hud_.SetActiveActionList(true);
        hud_.SetActiveAbilityList(false);

        DoPlayerAction();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(first_selected_button_);
    }

    public void DoPlayerAction()
    {
        state_ = BattleState.kPlayerAction;
        hud_.SetInteractableAbilityList(true);
    }

    public IEnumerator DoPlayerAbility(int idx)
    {
        Debug.Log(">>> Player used an ability!");
        var ability = player_unit_.GetBattleCreature().GetAbilities()[idx];
        if (!player_unit_.GetBattleCreature().SpendMP(ability)) //do nothing if not enough MP
        {
            hud_.UpdateCombatLog("Not enough MP!");
            Debug.Log(">>> Not enough MP!");
            yield break;
            Debug.Log(">>> Coroutine break!");
        }
        state_ = BattleState.kBusy;
        hud_.UpdateCombatLog("Player's " + player_unit_.base_.GetName() + " used " + ability.GetBase().GetName() + " on enemy " + enemy_unit_.base_.GetName());
        player_unit_.GetBattleCreature().DealDamage(ability);
        Animator animator = hud_.GetPlayerSpriteObj().transform.GetComponent<Animator>();
        string anim_state = (player_unit_.GetBattleCreature().GetBaseStats().GetAnimString(ability));
        animator.SetTrigger(anim_state);
        yield return new WaitForSeconds(1.0f);
        DamageResult dam_result = enemy_unit_.GetBattleCreature().TakeDamage(ability, player_unit_.GetBattleCreature());
        switch (dam_result)
        {
            case DamageResult.MissedDamage:
                hud_.UpdateCombatLog("Player's " + player_unit_.base_.GetName() + " MISSSED!");
                yield return new WaitForSeconds(1.0f);
                break;
            case DamageResult.TookDamage:
            case DamageResult.Death:
                hud_.GetEnemySpriteObj().transform.GetComponent<Animator>().SetTrigger("TakeHit");
                yield return new WaitForSeconds(1.0f);
                break;
        }
        hud_.UpdateMP(player_unit_.GetBattleCreature(), enemy_unit_.GetBattleCreature());
        hud_.UpdateHP(player_unit_.GetBattleCreature(), enemy_unit_.GetBattleCreature());

        if (dam_result == DamageResult.Death)
        {
            hud_.UpdateCombatLog("Enemy's " + enemy_unit_.base_.GetName() + " has fainted!");
            hud_.UpdateCombatLog("Player Wins!");
            hud_.UpdateCombatLog("Player's " + player_unit_.base_.GetName() + " gained "  + enemy_unit_.level_ * 5 + " experience!");
            Debug.Log(">>> ENEMY DEATH!");
            hud_.GetEnemySpriteObj().transform.GetComponent<Animator>().SetTrigger("Death");
            yield return new WaitForSeconds(3.0f);
            player_unit_.GainExperience(enemy_unit_.level_);
            enemy_unit_.KillCreature();
            enemy_unit_.gameObject.SetActive(false);
            OnBattleOver(true);
            rewardSystem.GenerateRewards(enemy_unit_.level_);
            yield break;
        }
        else
        {
            //ENEMY TURN
            hud_.SetInteractableAbilityList(false);
            StartCoroutine(DoEnemyTurn());
        }
    }

    public IEnumerator DoEnemyTurn()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log(">>> Enemy turn!");
        state_ = BattleState.kEnemyTurn;
        var ability = enemy_unit_.GetBattleCreature().GetRandAbility(); //do a random ability
        if (ability != null)
        {
            enemy_unit_.GetBattleCreature().SpendMP(ability);
            enemy_unit_.GetBattleCreature().DealDamage(ability);
            Animator animator = hud_.GetEnemySpriteObj().transform.GetComponent<Animator>();
            string anim_state = (enemy_unit_.GetBattleCreature().GetBaseStats().GetAnimString(ability));
            animator.SetTrigger(anim_state);
            if(ability.GetBase().GetAbilityType() != AbilityType.HealHp)
                hud_.UpdateCombatLog("Enemy's " + enemy_unit_.base_.GetName() + " used " + ability.GetBase().GetName() + " on player's " + player_unit_.base_.GetName());
            else
                hud_.UpdateCombatLog("Enemy's " + enemy_unit_.base_.GetName() + " used " + ability.GetBase().GetName() + " on itself!");
            yield return new WaitForSeconds(1.0f);
            DamageResult dam_result = player_unit_.GetBattleCreature().TakeDamage(ability, enemy_unit_.GetBattleCreature());
            switch (dam_result)
            {
                case DamageResult.MissedDamage:
                    hud_.UpdateCombatLog("Enemy's " + enemy_unit_.base_.GetName() + " MISSSED!");
                    yield return new WaitForSeconds(1.0f);
                    break;
                case DamageResult.TookDamage:
                case DamageResult.Death:
                    hud_.GetPlayerSpriteObj().transform.GetComponent<Animator>().SetTrigger("TakeHit");
                    yield return new WaitForSeconds(1.0f);
                    break;
            }
            hud_.UpdateMP(player_unit_.GetBattleCreature(), enemy_unit_.GetBattleCreature());
            hud_.UpdateHP(player_unit_.GetBattleCreature(), enemy_unit_.GetBattleCreature());

            if (dam_result == DamageResult.Death)
            {
                hud_.UpdateCombatLog("Players's " + player_unit_.base_.GetName() + " has fainted!");
                hud_.UpdateCombatLog("Enemy Wins!");
                Debug.Log(">>> PLAYER DEATH!");
                hud_.GetPlayerSpriteObj().transform.GetComponent<Animator>().SetTrigger("Death");
                yield return new WaitForSeconds(1.0f);
                OnBattleOver(false);
                yield break;
            }
            else
            {
                //PLAYER TURN
                DoPlayerAction();
            }
        }
        else
        {
            hud_.UpdateCombatLog("Enemy's " + enemy_unit_.base_.GetName() + " is stuck!");
            Debug.Log(">>> Enemy has no viable move");
            //PLAYER TURN
            DoPlayerAction();
        }
    }

    public void OnFightSelected()
    {
        hud_.SetActiveActionList(false);
        hud_.SetActiveAbilityList(true);
        hud_.SetInteractableAbilityList(true);
    }

    public void OnBackOutAbilityListSelected()
    {
        hud_.SetActiveActionList(true);
        hud_.SetActiveAbilityList(false);
    }

    public void OnFleeSelected()
    {
        OnBattleOver(true);
    }


    public void ResetHud()
    {
        hud_.ResetHud();
    }
    //public void OnAbilitySelected()
    //{
    //    state_ = BattleState.kEnemyAbility;
    //    hud_.SetInteractableAbilityList(false);
    //}
}

public enum BattleState
{
    kStart,
    kPlayerAction,
    kPlayerAbility,
    kEnemyTurn,
    kBusy
}