using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float offsetY = 0f; 
    public float offsetZ = -10f; 
    public float leftBoundary = -10f; 
    public float rightBoundary = 40f;
    public float playerYThreshold = 6f;
    public float cameraYOffset = 10.15f;

    private Camera cam;
    private float initialYPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialYPosition = transform.position.y + offsetY;
    }

    void Update()
    {
        if (player != null)
        {
            float cameraHalfWidth = cam.orthographicSize * Screen.width / Screen.height;

            float targetX = player.position.x;

            if (targetX - cameraHalfWidth < leftBoundary)
            {
                targetX = leftBoundary + cameraHalfWidth;
            }

            if (targetX + cameraHalfWidth > rightBoundary)
            {
                targetX = rightBoundary - cameraHalfWidth;
            }

            float targetY = initialYPosition;
            if (player.position.y > playerYThreshold)
            {
                targetY = cameraYOffset;
            }

            Vector3 newPosition = new Vector3(targetX, targetY, offsetZ);
            transform.position = newPosition;
        }
    }
}
