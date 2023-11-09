using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Vector2 movement;

    [SerializeField]
    protected float moveSpeed = 5f;
    [SerializeField]
    protected float jumpForce = 10f;

    private bool canJump = true;
    public float jumpCooldown = 1.0f;
    private float lastJumpTime = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

       if (!canJump && Time.time - lastJumpTime >= jumpCooldown)
        {
            canJump = true;
        }
        Jump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            lastJumpTime = Time.time;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}