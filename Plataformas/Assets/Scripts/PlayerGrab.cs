using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform grabPoint;
    public float grabDistance = 1.5f;
    public LayerMask boxLayer;

    private GameObject heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryGrab();
            }
            else
            {
                ReleaseBox();
            }
        }
    }

    void TryGrab()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grabDistance, boxLayer);

        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            heldObject = hit.collider.gameObject;
            heldObject.transform.SetParent(grabPoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void ReleaseBox()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;

            heldObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * grabDistance);
    }
}