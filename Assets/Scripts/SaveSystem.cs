using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Framework for saving
/// </summary>
public static class SaveSystem
{
    //https://youtu.be/XOjd_qU2Ido - SAVE & LOAD SYSTEM in Unity
    public static void SaveGameData(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string save_file_path = Application.persistentDataPath + "/player.dat";
        FileStream stream = new FileStream(save_file_path, FileMode.Create);

        SaveData data = new SaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGameData()
    {
        string save_file_path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(save_file_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(save_file_path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError(">>> No save file on path: " + save_file_path);
            return null;
        }
    }
}
