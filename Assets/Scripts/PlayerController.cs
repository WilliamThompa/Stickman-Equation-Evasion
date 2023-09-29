using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpHeight = 10f;
    public bool grounded;
    public Transform groundProbe;
    public float groundProbeRadius = 0.1f;
    public LayerMask groundLayer;

    //private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int jumpCount;
    private bool canDash;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;
    private int facingDirection;

    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jumpCount = 0;
        canDash = true;
        facingDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("speedY", rb.velocity.y);
        //anim.SetBool("Grounded", grounded);
        //anim.SetBool("Running", false);
        if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            facingDirection = 1;
            //anim.SetBool("Running", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            facingDirection = -1;
            //nim.SetBool("Running", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpCount++;
            //anim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (grounded)
        {
            jumpCount = 0;
        }

        if (isDashing)
        {
            return;
        }

        grounded = Physics2D.OverlapCircle(groundProbe.position, groundProbeRadius, groundLayer);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(facingDirection * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


}
