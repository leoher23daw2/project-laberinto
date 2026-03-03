using UnityEngine;
using TMPro;

public class Collectable : MonoBehaviour
{
    private TextMeshProUGUI collectableText;

    private void Start()
    {
        collectableText = GameObject.FindWithTag("CollectableText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.collectables += 1;
            collectableText.text = player.collectables.ToString();
            Destroy(gameObject);
            // Debug.Log("You caught a collectible"); // Mira sin has cogido un coleccionable.
        }
    }
}
