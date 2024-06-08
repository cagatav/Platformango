using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float lowGravityDuration = 10f;
    public float lowGravityScale = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = true;
    private bool gameIsOver = false;
    private bool canDoubleJump = false;
    private bool doubleJumpAvailable = false;
    private bool isOnIce = false;
    private float originalGravityScale;
    private float originalSpeed;

    private Vector3 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        originalSpeed = speed;

        respawnPosition = transform.position;
    }

    void Update()
    {
        if (gameIsOver)
        {
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("isJumping");
        }
        else if (!isGrounded && canDoubleJump && doubleJumpAvailable && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("isJumping");
            doubleJumpAvailable = false;
        }

        animator.SetFloat("yVelocity", rb.velocity.y);

        if (isOnIce)
        {
            rb.drag = -1f;
        }
        else
        {
            rb.drag = 1f;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJumpAvailable = canDoubleJump;
            animator.SetBool("isGrounded", true);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = true;
        }
        else if (collision.gameObject.CompareTag("Reward2"))
        {
            StartCoroutine(ApplyLowGravity());
        }
        else if (collision.gameObject.CompareTag("Reward1"))
        {
            canDoubleJump = true;
        }
        else if (collision.gameObject.CompareTag("Reward3"))
        {
            StartCoroutine(TemporarySpeedBoost());
        }
        else if (collision.gameObject.CompareTag("Up"))
        {
            MoveUp();
        }
        else if (collision.gameObject.CompareTag("Door2"))
        {
            jumpForce = 4f;
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnPosition = transform.position; 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        else if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        gameIsOver = true;
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        gameIsOver = false;
        transform.position = respawnPosition;
        rb.velocity = Vector2.zero;
    }

    private IEnumerator ApplyLowGravity()
    {
        rb.gravityScale = lowGravityScale;
        yield return new WaitForSeconds(lowGravityDuration);
        rb.gravityScale = originalGravityScale;
    }

    private void MoveUp()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
    }

    private IEnumerator TemporarySpeedBoost()
    {
        speed = 8f;
        yield return new WaitForSeconds(5f);
        speed = originalSpeed;
    }
}
