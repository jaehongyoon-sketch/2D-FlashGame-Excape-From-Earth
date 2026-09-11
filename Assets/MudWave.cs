using UnityEngine;

public class MudWave : MonoBehaviour
{
    public float waveSpeed = 12f; // 흙탕물 전진 속도

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0f; 
        
        rb.linearVelocity = new Vector2(-waveSpeed, 0f);

        
        Destroy(gameObject, 4.0f);
    }
}