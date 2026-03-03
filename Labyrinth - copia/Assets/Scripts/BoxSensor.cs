using UnityEngine;

public class BoxSensor : MonoBehaviour
{
    public GameObject door;
    public string targetTag = "Box";

    public Color activeColor = Color.green;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (door == null) 
        Debug.LogWarning("Attention! You have not assigned the door in the Inspector.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something has touched the sensor: " + collision.gameObject.name);

        if (isActivated) return;

        if (collision.CompareTag(targetTag))
        {
            Debug.Log("BOX DETECTED! Activating mechanism");
            isActivated = true;

            
            collision.transform.position = transform.position;
            Rigidbody2D boxRb = collision.GetComponent<Rigidbody2D>();
            if (boxRb != null)
            {
                boxRb.bodyType = RigidbodyType2D.Static;
                Debug.Log("Box locked in Static");
            }

            OpenDoor();

            if (spriteRenderer != null) spriteRenderer.color = activeColor;
        }
        else
        {
            Debug.Log("What has been entered does NOT have the tag " + targetTag + ". It has the tag: " + collision.tag);
        }
    }

    void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(false);
            Debug.Log("Vanished door");
        }
    }
}