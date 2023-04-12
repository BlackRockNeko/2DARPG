using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerInfo Player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/player.fun";
        FileStream Stream = new FileStream(Path, FileMode.Create);

        PlayerData data = new PlayerData(Player);

        formatter.Serialize(Stream, data);
        Stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Not Found in" + Path);
            return null;
        }
    }
}
