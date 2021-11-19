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

    // Update is called once per frame
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
            case BattleState.kEnemyAbility:
                hud_.SetInteractableAbilityList(true);
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

        state_ = BattleState.kPlayerAction;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(first_selected_button_);
    }

    public void OnFightSelected()
    {
        hud_.SetActiveActionList(false);
        hud_.SetActiveAbilityList(true);
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

    public void OnAbilitySelected()
    {
        state_ = BattleState.kEnemyAbility;
        hud_.SetInteractableAbilityList(false);
    }
}

public enum BattleState
{
    kStart,
    kPlayerAction,
    kPlayerAbility,
    kEnemyAbility,
    kBusy
}