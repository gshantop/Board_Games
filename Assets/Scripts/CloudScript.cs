using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float speed = 30f;        // Negative speed moves the cloud to the left
    public float floatHeight = 15f;  // How far the cloud drifts up and down
    public float floatSpeed = 0.5f;

    RectTransform cloud;
    RectTransform sky;
    float startY;
    float timeOffset;

    void Start()
    {
        cloud = GetComponent<RectTransform>();
        sky = transform.parent.GetComponent<RectTransform>();

        startY = cloud.anchoredPosition.y;

        // Every cloud starts at a different point of the wave, so they do not move in sync
        timeOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        Vector2 position = cloud.anchoredPosition;

        position.x += speed * Time.deltaTime;
        position.y = startY + Mathf.Sin((Time.time + timeOffset) * floatSpeed) * floatHeight;

        // When the cloud has left the screen, it comes back in from the other side
        float edge = (sky.rect.width + cloud.rect.width) / 2f;

        if (position.x > edge)
            position.x = -edge;
        else if (position.x < -edge)
            position.x = edge;

        cloud.anchoredPosition = position;
    }
}
