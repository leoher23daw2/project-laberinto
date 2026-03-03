using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    private TextMeshProUGUI keyText;
    public GameObject door;

    private void Start()
    {
        keyText = GameObject.FindWithTag("KeyText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.keys += 1;
            keyText.text = player.keys.ToString();
            Destroy(gameObject);
            Destroy(door);
            Debug.Log("You caught a key"); // Mira sin has cogido una llave.
            Debug.Log("The door has opened"); // Mira si se ha abierto la puerta.
        }
    }
}
