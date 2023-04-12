using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float Speed;
    public float StoppingDistance;
    public float StartHp;
    public float Hp;
    public Transform IdlePoint;
    private Transform Target;
    public Animator Anim;
    private Clicker clicker;
    private bool IsDie;
    public bool facingRight = true;
    public bool CanRun;
    public bool CanJump;
    public int Damege;

    AudioSource Audio;
    public AudioClip[] Sound; 

    [Range(0, 0.5f)]
    [SerializeField]
    private float CheckRadius = 0.5f;
    public LayerMask Ground;
    public LayerMask JumpClicker;
    public bool isGrounded;
    Rigidbody2D rb2D;
    public float JumpForce;

    public Transform GClick;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        clicker = GetComponentInChildren<Clicker>();
        Anim = GetComponentInChildren<Animator>();
        Audio = GetComponent<AudioSource>();
        Hp = StartHp;

        if (Target == null)
            Debug.LogWarning("Not Found Player...");
        if (clicker == null)
            Debug.LogWarning("Not Found Clicker...");
        if (Anim == null)
            Debug.LogWarning("Not Found Animator...");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GClick.position, CheckRadius, Ground);

        if (clicker.FollowPlayer == true && CanRun && !IsDie)
        {
            float Posx = transform.position.x - Target.position.x;

            if (!facingRight && Posx > 0)
            {
                Flip();
            }
            else if (facingRight && Posx < 0)
            {
                Flip();
            }

            if (Vector2.Distance(transform.position, Target.position) > StoppingDistance)
            {
                Anim.SetBool("IsRun", true);
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
            else
                Anim.SetBool("IsRun", false);
        }
        else if (!IsDie)
        {
            float Posx = transform.position.x - IdlePoint.position.x;

            if (!facingRight && Posx > 0)
            {
                Flip();
            }
            else if (facingRight && Posx < 0)
            {
                Flip();
            }

            if (transform.position.x != IdlePoint.position.x)
            {
                Anim.SetBool("IsRun", true);
                transform.position = Vector2.MoveTowards(transform.position, IdlePoint.position, Speed * Time.deltaTime);
            }
            else
            {
                Anim.SetBool("IsRun", false);
            }
        }

        if (facingRight)
        {
            RaycastHit2D GroundClickleft = Physics2D.Raycast(GClick.position, Vector2.left, 2f, JumpClicker);
            Debug.DrawRay(GClick.position, Vector2.left, Color.green);
            if (GroundClickleft.collider != null)
            {
                float PosXLeft = Mathf.Abs(transform.position.x - GroundClickleft.collider.transform.position.x);
                if (PosXLeft < 0.4f&&CanJump)
                {
                    if (isGrounded)
                        rb2D.velocity = Vector2.up * JumpForce;
                }
            }
        }
        else
        {
            RaycastHit2D GroundClickerRight = Physics2D.Raycast(GClick.position, Vector2.right, 2f, JumpClicker);
            Debug.DrawRay(GClick.position, Vector2.right, Color.green);
            if (GroundClickerRight.collider != null)
            {
                float PosXRight = Mathf.Abs(transform.position.x - GroundClickerRight.collider.transform.position.x);
                if (PosXRight < 0.3f && CanJump)
                {
                    if (isGrounded)
                        rb2D.velocity = Vector2.up * JumpForce;
                }
            }
        }

        RaycastHit2D GroundClickerdown = Physics2D.Raycast(GClick.position, Vector2.down, 10f, Ground);
        Debug.DrawRay(GClick.position, Vector2.down, Color.green);
        if (GroundClickerdown.collider == null)
        {
            CanRun = false;
            CanJump = false;
        }
        else
        {
            CanRun = true;
            CanJump = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void TakeDamage(int damege)
    {
        Hp -= damege;
        Audio.PlayOneShot(Sound[0]);
        if (facingRight)
        {
            rb2D.velocity = Vector2.right * 2f;
        }
        else
        {
            rb2D.velocity = Vector2.left * 2f;

        }

        if (Hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        IsDie = true;
        Anim.SetBool("Die", IsDie);
        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
    }
}
