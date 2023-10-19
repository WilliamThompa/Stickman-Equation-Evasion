using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 10f;
    private bool isFacingRight = true;

    private bool isJumping;
    private int maxJumps = 2;
    private int remainingJumps;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 34f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;

    private Animator anim;
    private bool anim_running;
    private bool isPunching;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim_running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            isJumping = false;
            remainingJumps = maxJumps;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        
        // Running Animations
        if (horizontal > 0 || horizontal < 0)
            anim_running = true;
        else
            anim_running = false;

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || (isJumping && remainingJumps > 0))
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                remainingJumps--;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isPunching = true;
        }
        else isPunching = false;

        anim.SetBool("Running", anim_running);
        anim.SetBool("Jumping", isJumping);
        anim.SetBool("Punching", isPunching);

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
