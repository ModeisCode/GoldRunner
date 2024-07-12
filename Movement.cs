using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnim;
    public Player player;

    private enum State
    {
        IDLE = 0,
        RUNNING = 1,
        JUMP = 2,
        ATTACK = 3
    };

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Hareket();
        Zıplama();
    }

    void Hareket()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        UpdateAnimation(moveX);
        FlipSprite(moveX);
    }

    void Zıplama()
    {
        //playerAnim.SetInteger("state", 1);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            playerAnim.SetTrigger("Jump");
            playerAnim.SetBool("jumping" , true);
            UpdateAnimationState(State.JUMP);
        }
    }

    private void UpdateAnimation(float moveX)
    {
        if (moveX != 0)
        {
            UpdateAnimationState(State.RUNNING);
        }
        else if (isGrounded)
        {
            playerAnim.SetBool("jumping" , false);
            UpdateAnimationState(State.IDLE);
        }
    }

    private void UpdateAnimationState(State state)
    {
        playerAnim.SetInteger("state", (int)state);
        // Debug.Log($"State changed to: {state}");
    }

    private void FlipSprite(float moveX)
    {
        if (moveX > 0) // Sağa bak
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0) // Sola bak
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("gold"))
        {
            Destroy(collision.gameObject);
            player.isSetGold = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}