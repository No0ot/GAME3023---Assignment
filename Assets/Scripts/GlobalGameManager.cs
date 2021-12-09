using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    //https://youtu.be/R9XMOEFne7w - Make A Game Like Pokemon in Unity | #12 - Starting and Ending Battles

    private PlayerController player_;
    [SerializeField] private BattleController battle_system_;
    private Camera main_cam_;

    private GlobalEnums.GameState game_state_;

    private void Awake()
    {
        player_ = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        //battle_system_ = GameObject.FindGameObjectsWithTag("BattleSystem")[0].GetComponent<BattleController>();
        main_cam_ = Camera.main;
        player_.OnEncountered += StartBattle;
        battle_system_.OnBattleOver += EndBattle;

        DoLoadGameData();
    }

    private void Start()
    {
        battle_system_.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (game_state_)
        {
            case GlobalEnums.GameState.FreeRoam:
                player_.DoUpdate();
                break;
            case GlobalEnums.GameState.Battle:
                battle_system_.DoUpdate();
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (game_state_)
        {
            case GlobalEnums.GameState.FreeRoam:
                player_.DoFixedUpdate();
                break;
            default:
                break;
        }
    }

    private void StartBattle()
    {
        Debug.Log(">>> StartBattle!!!");
        game_state_ = GlobalEnums.GameState.Battle;
        battle_system_.gameObject.SetActive(true);
        main_cam_.gameObject.SetActive(false);
        battle_system_.CreateBattle();
    }

    private void EndBattle(bool is_victory)
    {
        Debug.Log(">>> EndBattle!!!");
        battle_system_.gameObject.SetActive(false);
        main_cam_.gameObject.SetActive(true);
        battle_system_.battleStarted = false;
        battle_system_.ResetHud();
        game_state_ = GlobalEnums.GameState.FreeRoam;
    }

    public void DoSaveGameData()
    {
        SaveSystem.SaveGameData(player_);
    }

    public void DoLoadGameData()
    {
        SaveData data = SaveSystem.LoadGameData();

        player_.transform.position = new Vector3(data.player_pos[0], data.player_pos[1], data.player_pos[2]);
    }
}
