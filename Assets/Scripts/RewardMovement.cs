using UnityEngine;

public class RewardMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MakeTransparent();
            DisableCollider();
            StopAnimation();
        }
    }

    private void MakeTransparent()
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.2f;
            spriteRenderer.color = color;
        }
    }

    private void DisableCollider()
    {
        if (polygonCollider != null)
        {
            polygonCollider.enabled = false;
        }
    }

    private void StopAnimation()
    {
        if (animator != null)
        {
            animator.enabled = false;
    }
    }
}
