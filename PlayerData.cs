using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public float Hp;
    public float[] position;

    public PlayerData(PlayerInfo Player)
    {
        Hp = Player.Hp;

        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;
    }
}
