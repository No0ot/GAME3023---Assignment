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
    [SerializeField] private BattleUnit player_unit_;
    [SerializeField] private BattleUnit enemy_unit_;
    [SerializeField] private GameObject first_selected_button_;

    private BattleState state_;

    public event Action<bool> OnBattleOver;

    void Start()
    {
        SetupBattle();
    }

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

    public void SetupBattle()
    {
        player_unit_.Setup();
        enemy_unit_.Setup();
        hud_.SetPlayerData(player_unit_.GetBattleCreature());
        hud_.SetEnemyData(enemy_unit_.GetBattleCreature());
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

    public void DoPlayerAbility(int idx)
    {
        Debug.Log(">>> Player used an ability!");
        var ability = player_unit_.GetBattleCreature().GetAbilities()[idx];
        if (!player_unit_.GetBattleCreature().SpendMP(ability)) //do nothing if not enough MP
        {
            Debug.Log(">>> Not enough MP!");
            return;
        }
        state_ = BattleState.kBusy;
        bool is_death = enemy_unit_.GetBattleCreature().TakeDamage(ability, player_unit_.GetBattleCreature());
        hud_.UpdateMP();
        hud_.UpdateHP();
        if (is_death)
        {
            Debug.Log(">>> ENEMY DEATH!");
            OnBattleOver(true);
        }
        else
        {
            //ENEMY TURN
            hud_.SetInteractableAbilityList(false);
            DoEnemyTurn();
        }
    }

    public void DoEnemyTurn()
    {
        Debug.Log(">>> Enemy turn!");
        state_ = BattleState.kEnemyTurn;
        var ability = enemy_unit_.GetBattleCreature().GetRandAbility(); //do a random ability
        if (ability != null)
        {
            bool is_death = player_unit_.GetBattleCreature().TakeDamage(ability, player_unit_.GetBattleCreature());
            hud_.UpdateMP();
            hud_.UpdateHP();
            if (is_death)
            {
                Debug.Log(">>> PLAYER DEATH!");
                OnBattleOver(true);
            }
            else
            {
                //PLAYER TURN
                DoPlayerAction();
            }
        }
        else
        {
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