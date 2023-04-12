using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private int WhlieWeapon;

    public Animator[] Anim;

    AudioSource Audio;
    public AudioClip[] Clips;

    PlayerInfo Info;
    private float AttackTimer;
    private float AvoidTimer;
    public int AttackCount;
    public float AttackRange;
    private float ApNextRe;
    private float ApRe;
    private float CanMoveTimer;
    private int Damage;
    [SerializeField] private bool IsAttack;
    bool IsDefense;
    private PlayerMovement movement;
    bool IsInvincible;
    float InvincibleTimer;
    //[SerializeField] private bool CanCombo;
    bool IsDie;

    public GameObject GameOverUI;

    public Transform AttackPos;

    public LayerMask Enemies;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        GameObject PlayerInfo = GameObject.Find("PlayerInfo");
        Info = PlayerInfo.GetComponent<PlayerInfo>();
        Audio = GetComponent<AudioSource>();

        Info.Hp = Info.StartHp;
        Info.Ap = Info.StartAp;
        WhlieWeapon = 0;
        ApRe = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        defense();
        avoid();

        if (WhlieWeapon != movement.WhichWeapons)
        {
            WhlieWeapon = movement.WhichWeapons;
        }

        if (ApNextRe<Time.time)
        {
            ReloadAp();
        }

        switch (WhlieWeapon)
        {
            case 0:
                AttackRange = -0.15f;
                break;
            case 1:
                AttackRange = -0.25f;
                break;
        }

        if (CanMoveTimer < Time.time)
            movement.CanMove = true;

        if (InvincibleTimer < Time.time)
            IsInvincible = false;

        if (IsDie)
            movement.CanMove = false;
    }

    void Attack()
    {
        switch(WhlieWeapon)
        {
            case 0:
                if (Input.GetButtonDown("Attack") && !IsAttack && Info.Ap > 1 && !IsDie)
                {
                    Debug.Log("Attack!!");
                    UseAp(10);
                    IsAttack = true;
                    movement.CanMove = false;
                    //CanCombo = true;
                    //Anim.SetBool("CanCombo", CanCombo);
                    Anim[WhlieWeapon].SetTrigger("Attack");
                    Anim[WhlieWeapon].SetBool("IsAttack", IsAttack);
                    AttackTimer = 0.5f;
                    CanMoveTimer = Time.time + 0.6f;
                    StartCoroutine(AttackColdDown(AttackTimer));
                    Damage = 10;
                    Audio.PlayOneShot(Clips[0]);
                    /*
                    if (AttackCount > 0)
                    {
                        AttackCount = 0;
                        AttackCount++;
                    }
                    else
                        AttackCount++;
                    Anim.SetInteger("AttackCount", AttackCount);
                    */
                    Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemies);

                    foreach (Collider2D Enemy in EnemiseToDamage)
                    {
                        Enemy.GetComponent<EnemyAI>().TakeDamage(Damage);
                    }
                }
                /*
                else if (Input.GetButtonDown("Attack") && !IsAttack && CanCombo && Ap > 1)
                {
                    Debug.Log("Attack2!!");
                    ReloadAP = false;
                    UseAp(10);
                    IsAttack = true;
                    Anim.SetTrigger("Attack");
                    Anim.SetBool("IsAttack", IsAttack);
                    AttackTimer = 0.8f;
                    StartCoroutine(AttackColdDown(AttackTimer));
                    CanCombo = false;
                    Anim.SetBool("CanCombo", CanCombo);
                    //AttackCount = 0;
                    //Anim.SetInteger("AttackCount", AttackCount);
                    Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemies);
                }
                */
                break;
            case 1:
                if (Input.GetButtonDown("Attack") && !IsAttack && Info.Ap > 1 && !IsDie)
                {
                    Debug.Log("Attack!!");
                    UseAp(10);
                    IsAttack = true;
                    movement.CanMove = false;
                    //CanCombo = true;
                    Anim[WhlieWeapon].SetTrigger("Attack");
                    Anim[WhlieWeapon].SetBool("IsAttack", IsAttack);
                    AttackTimer = 0.8f;
                    CanMoveTimer = Time.time + 0.9f;
                    StartCoroutine(AttackColdDown(AttackTimer));
                    Damage = 20;
                    Audio.PlayOneShot(Clips[0]);
                    /*
                    if (AttackCount > 0)
                    {
                        AttackCount = 0;
                        AttackCount++;
                    }
                    else
                     AttackCount++;
                    Anim.SetInteger("AttackCount", AttackCount);
                    */
                    Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemies);

                    foreach (Collider2D Enemy in EnemiseToDamage)
                    {
                        Enemy.GetComponent<EnemyAI>().TakeDamage(Damage);
                    }

                }
                /*
                else if (Input.GetButtonDown("Attack") && !IsAttack && CanCombo && Ap > 1)
                {
                    Debug.Log("Attack2!!");
                    ReloadAP = false;
                    UseAp(10);
                    IsAttack = true;
                    Anim.SetTrigger("Attack");
                    Anim.SetBool("IsAttack", IsAttack);
                    AttackTimer = 1f;
                    StartCoroutine(AttackColdDown(AttackTimer));
                    CanCombo = false;
                    AttackCount = 0;
                    Anim.SetInteger("AttackCount", AttackCount);
                    Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemies);
                }
                */
                break;

        }
    }

    IEnumerator AttackColdDown(float Time)
    {
        yield return new WaitForSeconds(Time);

        IsAttack = false;
        Anim[WhlieWeapon].SetBool("IsAttack", IsAttack);
        //yield return new WaitForSeconds(0.8f);
        //CanCombo = false;
        //Anim.SetBool("CanCombo", CanCombo);
        //AttackCount = 0;
        //Anim.SetInteger("AttackCount", AttackCount);
    }

    void UseAp(int ap)
    {
        Info.Ap -= ap;

        if (Info.Ap < 0)
            Info.Ap = 0;

        ApNextRe = Time.time + ApRe;
    }

    void ReloadAp()
    {
        if (Info.Ap != 100)
            Info.Ap++;
    }

    void defense()
    {
        if (Info.Ap >= 10 && WhlieWeapon==1)
        {
            if (Input.GetButton("Defense"))
            {
                if (IsDefense != true)
                {
                    Anim[WhlieWeapon].SetTrigger("Defense");
                    Anim[WhlieWeapon].SetBool("IsDefense", true);
                    CanMoveTimer = Time.time + 0.9f;
                    movement.CanMove = false;
                    IsDefense = true;
                }
                else
                {
                    Anim[WhlieWeapon].SetBool("IsDefense", true);
                    CanMoveTimer = Time.time + 0.9f;
                    movement.CanMove = false;
                }
            }
            else
            {
                Anim[WhlieWeapon].SetBool("IsDefense", false);
                IsDefense = false;
            }
        }
    }

    void avoid()
    {
        if (Info.Ap > 1 && WhlieWeapon==0)
        {
            if (Input.GetButtonDown("Defense"))
            {
                UseAp(30);
                Anim[WhlieWeapon].SetTrigger("Avoid");
                StartCoroutine(avoidMove());
            }
        }
    }

    public void TakeDamage(int Damage)
    {
        if (!IsInvincible)
        {
            if (IsDefense)
            {
                Audio.PlayOneShot(Clips[1]);
                Info.Hp -= Damage / 2;
                IsInvincible = true;
                InvincibleTimer = Time.time + 1.5f;
            }
            else
            {
                Audio.PlayOneShot(Clips[1]);
                Info.Hp -= Damage;
                Anim[WhlieWeapon].SetTrigger("Hit");
                IsInvincible = true;
                InvincibleTimer = Time.time + 1.5f;
            }

            if (Info.Hp <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        IsDie = true;
        Anim[WhlieWeapon].SetBool("Die", IsDie);
        GameOverUI.SetActive(true);
    }

    IEnumerator avoidMove()
    {
        yield return new WaitForSeconds(0.2f);

        movement.CanMove = false;
        CanMoveTimer = Time.time + 0.8f;

        yield return new WaitForSeconds(0.3f);

        if (movement.facingRight == true)
            movement.rb2D.velocity = Vector2.right * 2f;
        else
            movement.rb2D.velocity = Vector2.left * 2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
}
