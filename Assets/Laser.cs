using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public GameObject warningEffect;
    public GameObject laserEffect;
    public BoxCollider2D col; 

    void Start()
    {
        // 시작할 때 초기화
        warningEffect.SetActive(false);
        laserEffect.SetActive(false);
        if (col != null) col.enabled = false;

        StartCoroutine(LaserRoutine());
    }

    IEnumerator LaserRoutine()
    {
        // 1. 경고 단계 (2초)
        float timer = 0;
        while (timer < 2.0f)
        {
            warningEffect.SetActive(!warningEffect.activeSelf);
            
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        // 2. 발사 단계
        warningEffect.SetActive(false);
        laserEffect.SetActive(true);
        if (col != null) col.enabled = true; 

        yield return new WaitForSeconds(0.2f);

        // 3. 삭제
        Destroy(gameObject);
    }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("레이저 피격!");

                
                if (col != null) col.enabled = false;

            }
        }
    
}
