using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startingPosition;
    private bool movingRight = false; 

    void Start()
    {
        startingPosition = transform.position;
        Flip();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Vector3.Distance(startingPosition, transform.position) >= distance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (Vector3.Distance(startingPosition, transform.position) >= distance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
