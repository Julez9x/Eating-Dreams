using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(TrailRenderer))]

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    private Vector3 start;

    [Header("Dash Parameters")]
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    [Header("Player Speed Parameters")]
    [SerializeField] private float accel;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float slowedSpeed;

    [Header("Player Jump Parameters")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpDuration;

    [Space(10)]
    [Header("Ground Check")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Misc")]
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private ParticleSystem dirtParticles;

    [SerializeField] private DashingMeter dashingMeter;
    [SerializeField] public Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        currentSpeed = normalSpeed;

        Vector3 start = rb.transform.position;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        //Mathf.Abs makes sure the number is always positive
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            CreateDirt();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        //Allows the player to jump as long as the hold the jump button for a set duration
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpDuration);
        }

        if (!IsGrounded())
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else if (IsGrounded()) 
        {
            playerAnimator.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);

        //speeds up player if "currentSpeed" is lower than "normalSpeed"
        if(currentSpeed <= normalSpeed)
        {
            currentSpeed += Time.deltaTime * accel;
        }
    }

    private bool IsGrounded()
    {
        //Places an invisible circle on the object to check if its on the ground
        //and when it collides with anything labled with the ground layer it allows
        //the player to jump
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Checks if the object is not facing right, if not facing right then it Flips the object
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            if (IsGrounded()) 
            { 
                CreateDirt();
            }
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
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        dashingMeter.EmptyMeter();
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Gas")
        {
            currentSpeed = slowedSpeed;
        }
    }

    void CreateDirt() 
    {
        dirtParticles.Play();
    }

    private void ResetPlayer()
    {
        rb.transform.position = start;
    }
}
