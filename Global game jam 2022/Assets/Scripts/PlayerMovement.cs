using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components and assignables")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [Header("Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distance = 1f;
    [SerializeField] private float input;
    [SerializeField] private float speedCap;
    public bool canMove = true;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Update()
    {
        AnimatePlyer();

        if (canMove)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void Movement()
    {
        //Gets the input direction and adds it to the player's velocity, multiplied by the movement speed
        input = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(input * movementSpeed, 0));
        if (Mathf.Abs(rb.velocity.x) > speedCap)
        {
            /*
             Velocity cap logic: if the player's velocity is above the speed cap, we set the velocity to the respected
             velocity cap, that being positive or negative
             */
            if (rb.velocity.x > speedCap)
            {
                rb.velocity = new Vector2(speedCap, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speedCap, rb.velocity.y);
            }
        }

        if (input == 0)
        {
            //Stops the player if no key is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void AnimatePlyer()
    {
        //If the input is more or less than 0, we animate the player
        if (input > 0)
        {
            transform.localScale = new Vector3(10, 10, 10);
            anim.SetBool(IsWalking, true);
        }
        else if (input < 0)
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
}
