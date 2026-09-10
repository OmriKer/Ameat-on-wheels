using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 1f;
    private bool isGrounded = false;
    public float rocketPower = 1000f;
    public float gas = 10f;

    public Collider2D groundCheckLeft;
    public Collider2D groundCheckRight;
    public Collider2D groundCheckBoth;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    Rigidbody2D wheelLeft;
    Rigidbody2D wheelRight;
    Transform rocket;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rocket = transform.Find("Rocket");
        wheelLeft = transform.Find("WheelLeft").GetComponent<Rigidbody2D>();
        wheelRight = transform.Find("WheelRight").GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Debug.Log(gas);
        // 1. Gather input every frame (WASD or Arrow Keys)
        movementInput.x = Input.GetAxisRaw("Horizontal");

        // Optional: Normalize the vector so diagonal movement isn't faster
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = 0f;
        }
    }

    void FixedUpdate()
    {
        if (groundCheckLeft.IsTouchingLayers() || groundCheckRight.IsTouchingLayers())
        {
            gas = 10f;
        }


        // 2. Apply movement physics at a fixed frame rate
        if (groundCheckBoth.IsTouchingLayers())
        {
            rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        }


        if (gas > 0f)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {

                Vector2 rocketDirection = rocket.position - transform.position;
                rb.AddForce(-rocketDirection.normalized * rocketPower, ForceMode2D.Force);
                gas -= 0.5f;
            }
        }


        if (rb.linearVelocity.y != 0)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                wheelLeft.AddTorque(10f);
                wheelRight.AddTorque(10f);
            }

            if (Keyboard.current.dKey.isPressed)
            {
                wheelLeft.AddTorque(-10f);
                wheelRight.AddTorque(-10f);
            }
        }

    }
}
