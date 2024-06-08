using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool gameIsOver = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gameIsOver && Input.anyKeyDown)
        {
            RestartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MakeOpaque();
            SetGameOver();
        }
    }

    private void MakeOpaque()
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    private void SetGameOver()
    {
        gameIsOver = true;
        Debug.Log("Game Over! Press any key to restart.");
    }

    private void RestartGame()
    {
        gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
