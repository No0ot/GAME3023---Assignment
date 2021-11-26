using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHudAbilityButtonController : MonoBehaviour
{
    private int id_ = -1;
    private BattleController battle_controller_;

    private void Awake()
    {
        battle_controller_ = FindObjectOfType<BattleController>();
    }

    public int GetId()
    {
        return id_;
    }

    public void SetId(int value)
    {
        id_ = value;
    }

    public void OnBattleHudAbilityButtonClicked()
    {
        if (id_ != -1)
        {
            battle_controller_.DoPlayerAbility(id_);
        }
    }
}
