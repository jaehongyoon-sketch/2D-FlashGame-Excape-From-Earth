using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed = 6f; 

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            rb.linearVelocity = new Vector2(0f, -fallSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
