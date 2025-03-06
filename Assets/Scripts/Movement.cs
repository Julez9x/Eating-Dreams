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

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpDuration;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private ParticleSystem dirtParticles;

    private ParticleSystem dirtParticalesInstance;

    [SerializeField] private DashingMeter dashingMeter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        Vector3 start = rb.transform.position;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            SpawnDirtParticles();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        //Allows the player to jump as long as the hold the jump button for a set duration
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpDuration);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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

    private void SpawnDirtParticles() 
    {
        Quaternion jumpDirection = Quaternion.FromToRotation(Vector3.left, Vector3.negativeInfinity);
        //Instantiate gets the particles and clones them when in use
        dirtParticalesInstance = Instantiate(dirtParticles, transform.position, Quaternion.identity);
    }

    private void ResetPlayer()
    {
        rb.transform.position = start;
    }
}
