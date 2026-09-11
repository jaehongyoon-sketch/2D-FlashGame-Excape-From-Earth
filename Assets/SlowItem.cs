using UnityEngine;
using System.Collections;

public class SlowItem : MonoBehaviour
{
    public float slowDuration = 12.0f; // 슬로우 지속 시간
    public float slowAmount = 0.5f;   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (GameManager.instance != null)
            {
                GameManager.instance.ActivateSlowMotion(slowDuration, slowAmount);
            }
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
