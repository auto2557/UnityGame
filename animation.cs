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
    [SerializeField]
    private Animator animator;
    private bool facingRight = true;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Flip()
    {
        // สลับค่า Scale ของ GameObject ในแกน X
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // สลับสถานะการหันไปทางซ้ายหรือขวา
        facingRight = !facingRight;
    }

    private void Update()
    {

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (!canJump && Time.time - lastJumpTime >= jumpCooldown)
        {
            canJump = true;
        }
        Jump();
        // ตรวจสอบการกดปุ่ม Horizontal (ซ้าย/ขวา)
        float horizontalInput = Input.GetAxis("Horizontal");

        // หากกดปุ่มไปทางซ้ายแต่ตัวละครหันไปทางขวา หรือ กดปุ่มไปทางขวาแต่ตัวละครหันไปทางซ้าย
        if ((horizontalInput < 0 && facingRight) || (horizontalInput > 0 && !facingRight))
        {
            // เรียกใช้ฟังก์ชัน Flip เพื่อ flip ภาพ
            Flip();
        }

        animator.SetBool("running", Mathf.Abs(horizontalInput) > 0);

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