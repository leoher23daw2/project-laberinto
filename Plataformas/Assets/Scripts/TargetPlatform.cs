using UnityEngine;

public class TargetPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color activeColor = Color.green; 
    private bool isActivated = false; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;

        if (other.CompareTag("Box"))
        {
            ActivatePlatform(other.gameObject);
        }
    }

    void ActivatePlatform(GameObject box)
    {
        isActivated = true;
        spriteRenderer.color = activeColor;

        box.transform.position = transform.position;

        Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
        if (boxRb != null)
        {
            boxRb.linearVelocity = Vector2.zero;
            boxRb.bodyType = RigidbodyType2D.Static; 
        }

        Debug.Log("Platform Activated!");
    }
}