using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what to be saved
/// </summary>
[System.Serializable]
public class SaveData
{
    //https://youtu.be/XOjd_qU2Ido - SAVE & LOAD SYSTEM in Unity
    public float[] player_pos;

    public SaveData(PlayerController player)
    {
        player_pos = new float[3];
        player_pos[0] = player.transform.position.x;
        player_pos[1] = player.transform.position.y;
        player_pos[2] = player.transform.position.z;
    }
}
