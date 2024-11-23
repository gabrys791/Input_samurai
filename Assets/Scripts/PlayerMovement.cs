using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private string controlScheme;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        controlScheme = PlayerPrefs.GetString("ControlScheme", "WASD");
    }
    private void Update()
    {
        //scoreCount.text = "Score: " + score.ToString();
        healthCount.text = "Health: " + health.currentHealth.ToString();
    }
    private void FixedUpdate()
    {
        horizontal = Gethorizontal();
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
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
        if (GetJump() && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
            
    }
    private float Gethorizontal()
    {
        switch(controlScheme)
        {
            case "WASD":
                return Input.GetAxisRaw("Horizontal");
            case "ESDF":
                if (Input.GetKey(KeyCode.F)) return 1f;
                if (Input.GetKey(KeyCode.S)) return -1f;
                break;
            case "DCSA":
                if (Input.GetKey(KeyCode.C)) return 1f;
                if (Input.GetKey(KeyCode.A)) return -1f;
                break;
            case "5678":
                if (Input.GetKey(KeyCode.Alpha8)) return 1f;
                if (Input.GetKey(KeyCode.Alpha5)) return -1f;
                break;
            case "SDFSpace":
                if (Input.GetKey(KeyCode.F)) return 1f;
                if (Input.GetKey(KeyCode.S)) return -1f;
                break;
        }
        return 0f;
    }
    private bool GetJump()
    {
        switch(controlScheme)
        {
            case "WASD":
                return Input.GetKey(KeyCode.W);
            case "ESDF":
                return Input.GetKey(KeyCode.E);
            case "5678":
                return Input.GetKey(KeyCode.Alpha6);
            case "DCSA":
                return Input.GetKey(KeyCode.S);
            case "SDFSpace":
                return Input.GetKey(KeyCode.Space);
        }
        return Input.GetKey(KeyCode.W);
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

        if(GetJump() && isWallSliding && !isGrounded())
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
        if(GetDash() && !isWalled())
        {
            if(dashCooldownTimer > 3f)
            {
                rb.AddForce(new Vector2(spriteRenderer.transform.localScale.x * 60f, 0f), ForceMode2D.Impulse);
                dashCooldownTimer = dashCooldown;
            }
            
        }
    }
    private bool GetDash()
    {
        switch(controlScheme)
        {
            case "WASD":
                return Input.GetKey(KeyCode.S);
            case "ESDF":
                return Input.GetKey(KeyCode.D);
            case "5678":
                return Input.GetKey(KeyCode.Alpha7);
            case "SDFSpace":
                return Input.GetKey(KeyCode.D);
            case "DCSA":
                return Input.GetKey(KeyCode.D);
        }
        return Input.GetKey(KeyCode.S);
    }
    private void Climb() //TODO: zrobiæ GETClimb do inputu
    {
        if(isWalled() && GetClimb() && climbTimer <= 1.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            climbTimer += Time.deltaTime;
        }
        if(climbTimer > 1.5f)
        {
            WallSlide();
        }
    }
    private bool GetClimb()
    {
        switch(controlScheme)
        {
            case "WASD":
                return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
            case "ESDF":
                return (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.F));
            case "5678":
                return (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Alpha8));
            case "SDFSpace":
                return (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.F));
            case "DCSA":
                return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.C));
        }
        return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
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
