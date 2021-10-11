using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    //https://youtu.be/zKRMkD28-xY - Make A Game Like Pokemon in Unity | #7 - Battle System Setup
    //https://youtu.be/SXBgBmUcTe0 - Using Menus with A Controller/Keyboard in Unity

    [SerializeField] private BattleHudController hud_;
    [SerializeField] private BattleUnit player_unit_;
    [SerializeField] private BattleUnit enemy_unit_;
    [SerializeField] private GameObject first_selected_button_;

    public event Action<bool> OnBattleOver;

    void Start()
    {
        SetupBattle();
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        //Debug.Log("meow");
    }

    public void SetupBattle()
    {
        player_unit_.Setup();
        enemy_unit_.Setup();
        hud_.SetPlayerData(player_unit_.GetBattleCreature());
        hud_.SetEnemyData(enemy_unit_.GetBattleCreature());

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(first_selected_button_);
    }

    public void OnFleeSelected()
    {
        OnBattleOver(true);
    }
}
