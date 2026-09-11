using UnityEngine;

public class Bomb : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.gravityScale *= (GameManager.globalSpeedMultiplier * GameManager.globalSpeedMultiplier);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
