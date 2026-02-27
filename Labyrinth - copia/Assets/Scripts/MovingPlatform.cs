using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;

    private int i;
    void Start()
    {
        transform.position = points[i].position;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        // Debug.Log("Moving Platform"); // Muesta cuando se mueve la plataforma.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            Debug.Log("Collision Enter"); // Muesta cuando está colisionando con la plataforma.
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        Debug.Log("Collision Exit"); // Muesta cuando no colisionando con la plataforma.
    }
}
