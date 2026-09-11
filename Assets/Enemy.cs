using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int direction = 1;
    public int scoreValue = 100; // 적 처치 시 획득할 점수
    public GameObject scoreTextPrefab; 

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb != null)
        {
            float currentMoveSpeed = -direction * speed * GameManager.globalSpeedMultiplier;
            rb.linearVelocity = new Vector2(currentMoveSpeed, rb.linearVelocity.y);
        }
    }
    // 플레이어에 닿으면 게임오버
    void OnCollisionEnter2D(Collision2D collision)
        {

        if (collision.gameObject.CompareTag("Player"))
            {
            Debug.Log("플레이어랑 충돌!");
            }
        }
 }
