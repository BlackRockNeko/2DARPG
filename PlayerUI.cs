using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerInfo Info;
    public Image HpBar;
    public Image ApBar;

    private void Start()
    {
        GameObject PlayerInfo = GameObject.Find("PlayerInfo");
        Info = PlayerInfo.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = Info.Hp / Info.StartHp;
        ApBar.fillAmount = Info.Ap / Info.StartAp;
    }
}
