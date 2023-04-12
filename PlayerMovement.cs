using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator[] Anim;
    public GameObject[] PlayerCharater;
    public Rigidbody2D rb2D;
    [SerializeField] private float Speed = 5f; //Move Speed
    [SerializeField] private float JumpForce = 10f; //Jump Power

    AudioSource Audio;
    public AudioClip[] Sounds;

    public bool facingRight = true;

    [SerializeField] private bool isGrounded;
    public bool CanMove;
    public Transform GroundChecker;
    [Range(0,0.5f)]
    [SerializeField]
    private float CheckRadius = 0.5f;
    public LayerMask Ground;

    private int JumpTime; //MuitJump
    [SerializeField] int SetJumpTime = 1; //MuitJumpSetup
    public int WhichWeapons;

    private void Start()
    {
        JumpTime = SetJumpTime;
        rb2D = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        CanMove = true;
    }

    private void Update()
    {
        if (CanMove == true)
        {
            Movement();
            Jumping();
        }

        if (Input.GetKeyDown(KeyCode.Backspace)&&Input.GetKeyDown(KeyCode.Backslash))
        {
            if (WhichWeapons == 0)
            {
                WhichWeapons = 1;
                PlayerCharater[1].SetActive(true);
                PlayerCharater[0].SetActive(false);
            }
            else
            {
                WhichWeapons = 0;
                PlayerCharater[1].SetActive(false);
                PlayerCharater[0].SetActive(true);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Movement()
    {
        float Movement = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(Movement * Speed, rb2D.velocity.y);

        if (Movement == 0)
        {
            Anim[WhichWeapons].SetBool("IsRun", false);
        }
        else
        {
            Anim[WhichWeapons].SetBool("IsRun", true);
        }

        if (!facingRight && Movement > 0)
        {
            Flip();
        }
        else if (facingRight && Movement < 0)
        {
            Flip();
        }
    }

    void Jumping()
    {
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, CheckRadius, Ground);

        if (isGrounded)
        {
            Anim[WhichWeapons].SetBool("IsJump", false);
            JumpTime = SetJumpTime;
        }
        else
        {
            Anim[WhichWeapons].SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Jump") && JumpTime > 0)
        {
            rb2D.velocity = Vector2.up * JumpForce;
            JumpTime--;
            Anim[WhichWeapons].SetTrigger("Jump");
            Audio.PlayOneShot(Sounds[0]);
            
        }
        else if (Input.GetButtonDown("Jump") && JumpTime == 0 && isGrounded)
        {
            rb2D.velocity = Vector2.up * JumpForce;
            Anim[WhichWeapons].SetTrigger("Jump");
            Audio.PlayOneShot(Sounds[0]);
        }
    }
}
