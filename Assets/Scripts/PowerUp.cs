using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Start()
    {
        // Set a random color or any desired attribute for the collectible
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
    }

    // Can be triggered by the player to increase the number of rockets
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the power-up after collision
        }
    }
}
