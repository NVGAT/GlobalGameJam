using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components and assignables")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource dashSource;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject dashFrames;
    [Header("Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distance = 1f;
    [SerializeField] private float inputX;
    [SerializeField] private float inputY;
    [SerializeField] private float speedCap;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float verticalDashMultiplier;
    [SerializeField] private float dashFrameFollowSpeed;
    public bool isDashing;
    private float dashTimer;
    public bool canMove = true;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void FixedUpdate()
    {
        try
        {
            dashFrames.transform.position = Vector3.Lerp(dashFrames.transform.position, transform.position, dashFrameFollowSpeed);
        }
        catch (NullReferenceException)
        {
            //the only way i get rid of the errors in the inspector :)
        }
        catch (UnassignedReferenceException)
        {
            //inspector errors :)
        }
    }

    private void Update()
    {
        dashTimer += Time.deltaTime;
        AnimatePlyer();

        if (canMove)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer > dashCooldown)
        {
            Dash();
        }
    }

    void Movement()
    {
        //Gets the input direction and adds it to the player's velocity, multiplied by the movement speed
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector2((inputX * movementSpeed) * Time.deltaTime, 0));
        //Velocity cap logic: if the absolute value of the x velocity is above the speed cap, and we're not dashing, we can proceed
        if (Mathf.Abs(rb.velocity.x) > speedCap && !isDashing)
        {
            //If the x velocity is more than the speed cap, that means it's positive and should be set to the velocity cap
            if (rb.velocity.x > speedCap)
            {
                rb.velocity = new Vector2(speedCap, rb.velocity.y);
            }
            //However, if it's less than the speed cap, that means it's negative and should be set to the negative velocity cap
            else
            {
                rb.velocity = new Vector2(-speedCap, rb.velocity.y);
            }
        }

        if (inputX == 0)
        {
            //Stops the player if no key is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Dash()
    {
        dashFrames.SetActive(true);
        StartCoroutine(cameraShake.Shake(.15f, .4f));
        dashSource.Play();
        //We set the dashing bool to be true, reset the dash timer and store the pre-dash velocity in a Vector2
        isDashing = true;
        dashTimer = 0;
        Vector2 originalVelocity = rb.velocity;
        //We calculate the post-dash velocity. The formula is input*dashForce for X and (input*force)*verticalDashMultiplier for Y
        Vector2 dashVelocity = new Vector2(dashForce * inputX, (dashForce * inputY) * verticalDashMultiplier);
        rb.AddForce(dashVelocity);
        //Resets the player's velocity after the dash
        StartCoroutine(SetVelocityAfterTime(dashDuration, originalVelocity));

    }

    void AnimatePlyer()
    {
        //If the input is more or less than 0, we animate the player
        if (inputX > 0)
        {
            transform.localScale = new Vector3(10, 10, 10);
            anim.SetBool(IsWalking, true);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-10, 10, 10);
            anim.SetBool(IsWalking, true);
        }
        //On the other hand, if the input is 0, we don't animate the player
        else
        {
            anim.SetBool(IsWalking, false);
        }
        anim.SetBool(IsJumping, !IsGrounded());
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    bool IsGrounded()
    {
        //Raycast value things
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        //Shoots a ray down from the player and checks if it interferes with anything of the ground layer
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            //If the object that the raycast hit had a collider we return true, otherwise we return false
            return true;
        }

        return false;
    }

    IEnumerator SetVelocityAfterTime(float time, Vector2 velocity)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = velocity;
        isDashing = false;
        dashFrames.SetActive(false);
    }
}
