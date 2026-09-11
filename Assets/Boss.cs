using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Image warningFlashImage;
    public GameObject hpBarObject;
    public Slider hpBar;
    [Header("Warning Settings")]
    public GameObject MeteorWarningLinePrefab;
    public GameObject MudWaveWarningLinePrefab;
    [Header("Boss Stats")]
    public int maxHp = 200;
    private int currentHp;

    [Header("Pattern Prefabs")]
    public GameObject meteorPrefab;     
    public GameObject mudWavePrefab;    

    private bool isDead = false;

    
    void OnEnable()
{

    hpBarObject.SetActive(true);
    currentHp = maxHp;
    isDead = false;

    hpBar.maxValue = maxHp;
    hpBar.value = currentHp;

    Debug.Log($"보스 등장! 체력: {currentHp}");

    StartCoroutine(BossEntranceRoutine());
}

    IEnumerator BossEntranceRoutine()
{
    for (int i = 0; i < 5; i++)
    {
        warningFlashImage.gameObject.SetActive(true);

        warningFlashImage.color =
            new Color(1f, 0f, 0f, 0.55f);

        yield return new WaitForSecondsRealtime(0.3f);

        warningFlashImage.color =
            new Color(1f, 0f, 0f, 0f);

        yield return new WaitForSecondsRealtime(0.3f);
    }

    StartCoroutine(BossPatternRoutine());
}

    IEnumerator BossPatternRoutine()
    {
       
        yield return new WaitForSeconds(3.0f);

        while (!isDead)
        {
            
            int randomPattern = Random.Range(0, 2);

            if (randomPattern == 0)
            {
                Debug.Log("보스 패턴 1: 운석 낙하!");
                yield return StartCoroutine(MeteorPattern());
            }
            else
            {
                Debug.Log("보스 패턴 2: 흙탕물 전진!");
                yield return StartCoroutine(MudWavePattern());
            }

           
            yield return new WaitForSeconds(1.0f);
        }
    }

    // [패턴 1] 하늘에서 운석 14개가 무작위 위치로 쏟아짐
    IEnumerator MeteorPattern()
    {
        // 1. 운석이 떨어질 무작위 X 위치 14개를 미리 뽑아서 경고선을 배치합니다.
        float[] spawnXPositions = new float[14];
        for (int i = 0; i < 14; i++)
        {
            spawnXPositions[i] = Random.Range(-13f, 13f);
            Vector3 warnPos = new Vector3(spawnXPositions[i], 0.2f, -1f); // 바닥 높이에 경고 표시

            if (MeteorWarningLinePrefab != null)
            {
                Instantiate(MeteorWarningLinePrefab, warnPos, Quaternion.identity);
            }
        }

    
        yield return new WaitForSeconds(1.0f);

       
        for (int i = 0; i < 14; i++)
        {
            Vector3 spawnPos = new Vector3(spawnXPositions[i], 10f, 0f); 
            if (meteorPrefab != null)
            {
                Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f); 
        }
    }

    // [패턴 2] 흙탕물이 오른쪽 끝에서 왼쪽 끝으로 슥 지나감
    IEnumerator MudWavePattern()
    {
        
        if (MudWaveWarningLinePrefab != null)
        {
            // 흙탕물이 올 거라는 경고 표시 생성
            Vector3 warnPos = new Vector3(0f, 0.2f, -1f);
            GameObject MudWaveWarningLine = Instantiate(MudWaveWarningLinePrefab, warnPos, Quaternion.identity);

           
            MudWaveWarningLine.transform.localScale = new Vector3(30f, MudWaveWarningLine.transform.localScale.y, 1f);

    
        }

        
        yield return new WaitForSeconds(1.0f);

        
        if (mudWavePrefab != null)
        {
            Vector3 spawnPos = new Vector3(12f, 0.2f, 0f); 
            Instantiate(mudWavePrefab, spawnPos, Quaternion.identity);
        }
    }
    
    public void TakeDamage()
    {
        if (isDead) return;

        currentHp--;
        hpBar.value = currentHp;
        Debug.Log($"보스가 공격당함! 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            BossDie();            
        }
    }

    void BossDie()
    {
        hpBarObject.SetActive(false);
        isDead = true;
        StopAllCoroutines();
        Debug.Log("★ 보스 처치 완료! 승리! ★");

    
        Destroy(gameObject);
    }
}
