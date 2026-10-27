
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public int maxSpeed = 10;

    [Header("Jump")]
    public float jumpForce = 10f;
    private bool isGrounded = false;
    private bool jumpRequested = false; // Флаг, что игрок нажал Пробел
    private bool facingRight = true;

    void Update()
    {
        if (Keyboard.current == null) return;

        // ПРАВИЛЬНО: Ловим нажатие Пробела в Update, чтобы Unity его точно не пропустил
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            jumpRequested = true;
        }
        if (rb.linearVelocity.magnitude > maxSpeed) {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }
    void FixedUpdate()
    {
        OnMove();
    }
    void OnMove() {
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce(Vector2.right * moveSpeed);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce(-Vector2.right * moveSpeed);
        }
        if (jumpRequested)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpRequested = false;
        }
        if (facingRight == true && Keyboard.current.aKey.isPressed)
        {
            Flip();
        }
        else if (facingRight == false && Keyboard.current.dKey.isPressed) {
            Flip();
        }
    }
    public void CheckGrounded(Collision2D cols) {
        if (cols.collider.CompareTag("ground")) {
            isGrounded = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGrounded(collision);        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
    private void Flip() {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}