using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{

    public GameObject HealthBar;
    public Image Bar;
    public bool FollowPlayer;
    EnemyAI AI;

    private void Start()
    {
        AI = GetComponentInParent<EnemyAI>();
        HealthBar.SetActive(false);
    }

    private void Update()
    {
        Bar.fillAmount = AI.Hp / AI.StartHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FollowPlayer = true;
            HealthBar.SetActive(true);
            Debug.Log("Player Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FollowPlayer = false;
            HealthBar.SetActive(false);
            Debug.Log("Player Exit");
        }
    }
}
