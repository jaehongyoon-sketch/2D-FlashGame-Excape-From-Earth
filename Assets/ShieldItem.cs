using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                
                player.ActivateShield();
                Debug.Log("아이템 획득: 보호막 활성화!");
            }

            
            Destroy(gameObject);
        }
    }
}