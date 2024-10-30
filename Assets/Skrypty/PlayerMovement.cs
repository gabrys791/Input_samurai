using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight;
    private BoxCollider2D boxCollider2D;

    private bool isWallSliding;
    private float WallSlidespeed = 0.1f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpCooldown = 0f;
    private float wallJumpCooldownTimer;
    private Vector2 wallJumpingPower = new Vector2(4f, 8f);

    private float dashCooldown = 0f;
    private float dashCooldownTimer;

    private float climbTimer = 0f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    SpriteRenderer spriteRenderer;
    public int score = 0;
    public Text scoreCount;
    private Animator animator;
    private HealthManager health;
    public Text healthCount;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
    }
    private void Update()
    {
        //scoreCount.text = "Score: " + score.ToString();
        healthCount.text = "Health: " + health.currentHealth.ToString();
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (isGrounded() && !isWalled())
        {
            Jump();
        }
        else if (isWalled() && !isGrounded())
        {
            WallSlide();
            WallJump();
        }
        Flip();
        if (isGrounded())
        {
            climbTimer = 0f;   
        }
        if (wallJumpCooldownTimer >= 0f)
        {
            wallJumpCooldownTimer += Time.deltaTime;
        }
        Dash();
        if(dashCooldownTimer >= 0f)
        {
            dashCooldownTimer += Time.deltaTime;
        }
        Climb();
        if(climbTimer > 1.5f)
        {
            WallSlide();
        }
        animator.SetBool("run", horizontal != 0);
        animator.SetBool("grounded", isGrounded());
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.J) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
            
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit2D.collider;
    }
    private bool isWalled()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0),  0.2f, wallLayer);
        return raycastHit2D.collider;
    }
    private bool IsObstacled()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, obstacleLayer);
        return raycastHit2D.collider;
    }
    private void WallSlide()
    {
        if(isWalled() && !isGrounded())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidespeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -Mathf.Sign(spriteRenderer.transform.localScale.x);
        }

        if(Input.GetKey(KeyCode.J) && isWallSliding && !isGrounded())
        {
            if (wallJumpCooldownTimer > 2f)
            {
                isWallJumping = true;
                rb.velocity = new Vector2(-spriteRenderer.transform.localScale.x * 40f, 5f);
                //spriteRenderer.flipX = true;
                isFacingRight = !isFacingRight;
                Vector3 localScale = spriteRenderer.transform.localScale;
                localScale.x *= -1f;
                spriteRenderer.transform.localScale = localScale;
                wallJumpCooldownTimer = wallJumpCooldown;
                climbTimer = 0f;
            }
        }
    }
    private void Dash()
    {
        if(Input.GetKey(KeyCode.W) && !isWalled())
        {
            if(dashCooldownTimer > 3f)
            {
                rb.AddForce(new Vector2(spriteRenderer.transform.localScale.x * 60f, 0f), ForceMode2D.Impulse);
                dashCooldownTimer = dashCooldown;
            }
            
        }
    }
    private void Climb()
    {
        if(isWalled() && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && climbTimer <= 1.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            climbTimer += Time.deltaTime;
        }
        if(climbTimer > 1.5f)
        {
            WallSlide();
        }
    }
    private void Flip()
    {
        if(isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = spriteRenderer.transform.localScale;
            localScale.x *= -1f;
            spriteRenderer.transform.localScale = localScale;
        }
    }
}
