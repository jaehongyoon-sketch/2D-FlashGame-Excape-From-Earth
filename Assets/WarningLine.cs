using UnityEngine;
using System.Collections;

public class WarningLine : MonoBehaviour
{
    public GameObject bombPrefab; 

    void Start()
    {
        StartCoroutine(SpawnBombRoutine());
    }

    IEnumerator SpawnBombRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        if (bombPrefab != null)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 10f, 0f);
            Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
