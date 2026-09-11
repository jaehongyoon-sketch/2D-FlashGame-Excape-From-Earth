using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public GameObject laserPrefab;
    public GameObject WarningLinePrefab;
    public GameObject B2Prefab;
    public GameObject slowItemPrefab;
    public GameObject shieldItemPrefab;

    float spawnInterval = 4f;
    float timer = 0f;
    float enemySpeed = 3f;

    void Start()
    {
        StartCoroutine(WaitAndStartLaser());
        StartCoroutine(WaitAndStartBomber());
    }

    IEnumerator WaitAndStartLaser()
    {
        yield return new WaitForSeconds(20.0f);
        InvokeRepeating("SpawnLaser", 0f, 4.0f);
        Debug.Log("이제부터 레이저가 소환됩니다!");
    }

    IEnumerator WaitAndStartBomber()
    {
        yield return new WaitForSeconds(40.0f);
        while (true)
        {
            StartCoroutine(BomberEventRoutine());
            yield return new WaitForSeconds(12.0f);
        }
    }

    IEnumerator BomberEventRoutine()
    {
        int bombCount = Random.Range(2, 6);
        for (int i = 0; i < bombCount; i++)
        {
            float randomX = Random.Range(-13f, 13f);
            Vector3 warnPos = new Vector3(randomX, 0, 0);
            Instantiate(WarningLinePrefab, warnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(2.0f);
        Instantiate(B2Prefab, new Vector3(-12f, 10f, 0f), Quaternion.identity);
    }

    void SpawnLaser()
    {
        float randomY = Random.Range(0f, 2f);
        Vector3 spawnPos = new Vector3(0, randomY, 0);
        Instantiate(laserPrefab, spawnPos, Quaternion.identity);
    }
    public void StopAllSpawning()
    {
        
        this.enabled = false;

        
        CancelInvoke("SpawnLaser");

       
        StopAllCoroutines();

        Debug.Log("Spawner의 모든 생성 기능이 안전하게 멈췄습니다.");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
            IncreaseDifficulty(); 
        }
    }

    void SpawnEnemy()
    {
        int side = Random.Range(0, 2);
        Vector2 spawnPos;
        int dir;

        float fixedY = 0.3f;

        if (side == 0) { spawnPos = new Vector2(-14, fixedY); dir = -1; }
        
        else { spawnPos = new Vector2(14, fixedY); dir = 1; }

        
        if (slowItemPrefab != null && Time.timeSinceLevelLoad > 50.0f && Random.value < 0.05f)
        {
            float itemX = Random.Range(-11f, 11f);
            Instantiate(slowItemPrefab, new Vector3(itemX, 0.03f, 0), Quaternion.identity);

            Debug.Log("아이템 생성됨!");
        }

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
            {
                e.direction = dir;
                e.speed = enemySpeed;
            }
        }
        if (shieldItemPrefab != null && Time.timeSinceLevelLoad > 55.0f && Random.value < 0.05f)
        {
            float itemX = Random.Range(-11f, 11f);
            Instantiate(shieldItemPrefab, new Vector3(itemX, 0.1f, 0), Quaternion.identity);
        }
    } // SpawnEnemy 함수 끝

    void IncreaseDifficulty() 
    {
        enemySpeed += 0.07f;
        spawnInterval -= 0.08f;
        if (spawnInterval < 0.9f)
            spawnInterval = 0.9f;
    }
}