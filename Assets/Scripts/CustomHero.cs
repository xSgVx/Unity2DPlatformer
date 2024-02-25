using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHero : MonoBehaviour
{
    private float speed = 3f;
    private float lives = 5;
    private float jumpForce = 10f;
    private bool isGrounded = false;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    public State State
    {
        get { return (State)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Run()
    {
        if (isGrounded) State = State.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded) State = State.idle;

        if (Input.GetButton("Horizontal"))
            Run();

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = State.jump;
    }


}

public enum State
{
    idle = 0,
    jump = 2,
    run = 1
}