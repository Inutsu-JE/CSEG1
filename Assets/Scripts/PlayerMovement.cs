using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    //Player Parameters
    private float moveHorizontal;
    private float playerSpeed = 7f;
    private float jumpingPower = 13f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //Player Animation states
    public Animator _animator;
    public AudioSource SFX;
    public AudioClip playerIdle, playerJump, playerDash;

    private void Start() 
    {
        SFX.clip = playerIdle;
        SFX.Play();
    }


    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            _animator.SetBool("IsJumping", true);
            SFX.clip = playerJump;
            SFX.Play();

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            _animator.SetBool("IsDashing", true);
            SFX.clip = playerDash;
            SFX.Play();

        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(moveHorizontal * playerSpeed, rb.velocity.y);
        _animator.SetBool("IsDashing", false);

        if (IsGrounded()) 
        {
            return;
        }
        _animator.SetBool("IsJumping", false);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void Flip()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
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
