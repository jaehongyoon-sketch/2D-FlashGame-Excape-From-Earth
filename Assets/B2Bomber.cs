using UnityEngine;

public class B2Bomber : MonoBehaviour
{
    public float speed = 30f;

    void Start()
    {
       
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        
        transform.Translate(Vector2.right * (speed * GameManager.globalSpeedMultiplier) * Time.deltaTime);
    }
}
