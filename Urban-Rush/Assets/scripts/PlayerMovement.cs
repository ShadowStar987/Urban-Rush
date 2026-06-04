using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --- Settings you can change in Unity Inspector ---
    public float moveSpeed = 8f;        // How fast the player walks
    public float jumpForce = 16f;       // How high the player jumps
    public LayerMask groundLayer;       // What counts as ground

    // --- Private variables (used internally) ---
    private Rigidbody2D rb;             // Reference to the physics component
    private BoxCollider2D col;          // Reference to the collider
    private float moveInput;            // Stores left/right input
    private bool isGrounded;            // Is the player on the ground?

    void Start()
    {
        // Grab the components on the player object
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Get left/right input (-1 = left, 1 = right, 0 = nothing)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if the player is standing on the ground
        isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

        // Jump when Space is pressed and player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Move the player left or right using physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
