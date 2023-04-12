using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttck : MonoBehaviour
{
    EnemyAI AI;
    public bool HaveAttackAnim;

    public Transform AttackPos;
    public float AttackRange;
    public float AttackTime;

    public LayerMask Enemy;

    private void Start()
    {
        AI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (HaveAttackAnim)
            {
                StartCoroutine(AttackHasAttackAnim(AttackTime));
            }
            else
                StartCoroutine(Attack(AttackTime));
        }
    }

    IEnumerator AttackHasAttackAnim(float AttckTime)
    {
        AI.Anim.SetTrigger("Attack");

        yield return new WaitForSeconds(AttckTime);

        Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemy);

        foreach (Collider2D Enemy in EnemiseToDamage)
        {
            Enemy.GetComponent<PlayerCombat>().TakeDamage(AI.Damege);
        }
    }

    IEnumerator Attack(float AttckTime)
    {
        yield return new WaitForSeconds(AttckTime);

        Collider2D[] EnemiseToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, Enemy);

        foreach (Collider2D Enemy in EnemiseToDamage)
        {
            Enemy.GetComponent<PlayerCombat>().TakeDamage(AI.Damege);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
}
