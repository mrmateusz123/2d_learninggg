using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jump_velocity = 10;
    public int extrajumps = 1;
    private int extrajump;
    public float speed = 8;
    private float dirX;
    private bool has2jump = false;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer SpriteRenderer;
    private BoxCollider2D coll;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource WalkingSoundEffect;

    [SerializeField] private LayerMask jumpableGround;

    private enum Movement_State { Idle, Running, Jumping, Falling, Second_Jump}
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        extrajump = extrajumps;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && ground())
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
        }

        else if(Input.GetButtonDown("Jump") && extrajump > 0) 
        {
            extrajump--;
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, (jump_velocity * 3) / 4);
            has2jump = true;
        }
        UpdateAnimationState();

    }
    private void UpdateAnimationState()
    {
        Movement_State state;

        if (dirX > 0f)
        {
            state = Movement_State.Running;
            SpriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = Movement_State.Running;
            SpriteRenderer.flipX = true;
        }
        else
        {
            state = Movement_State.Idle;
        }
        if (has2jump)
        {
            state = Movement_State.Second_Jump;
        }
        else if (rb.velocity.y > .1f )
        {
            state = Movement_State.Jumping;
        }
        if (rb.velocity.y < -.1f)
        {
            state = Movement_State.Falling;
        }


        animator.SetInteger("State", (int)state);
    }
    private void has2jumps()
    {
        has2jump = false;
    }
    private bool ground()
    {
        return Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, jumpableGround);
    }
}
