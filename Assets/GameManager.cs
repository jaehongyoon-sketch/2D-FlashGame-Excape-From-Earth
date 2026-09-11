using UnityEngine;
using System.Collections;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Boss Stage Settings")]
    public Spawner spawner;             
    public GameObject bossObject;      
    private bool isBossStageStarted = false;
    private int currentScore = 0;
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public bool isGameOver = false;

    public static float globalSpeedMultiplier = 1.0f;
    public int score = 0;
    public void SubtractScore()
    {
        if (isGameOver) return;
        currentScore -= 50;
        if (currentScore < 0) currentScore = 0;
        UpdateScoreUI();
    }
    private void ClearRemainingObstacles()
    {
       
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies) Destroy(e.gameObject);

    
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb b in bombs) Destroy(b.gameObject);

        
        WarningLine[] warnings = FindObjectsOfType<WarningLine>();
        foreach (WarningLine w in warnings) Destroy(w.gameObject);

       
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject l in lasers) Destroy(l);

        
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script.GetType().Name == "B2Bomber" || script.CompareTag("B2"))
            {
                Destroy(script.gameObject);
            }
        }
    }
    private void StartBossStage()
{
    isBossStageStarted = true;

    Debug.Log("★ 보스 스테이지 시작 ★");

    if (spawner != null)
    {
        spawner.StopAllSpawning();
    }

    ClearRemainingObstacles();

    if (bossObject == null)
    {
     
        return;
    }


    bossObject.SetActive(true);

}

    public void AddScore(int score)
    {
        if (isGameOver) return;
        currentScore += score; // 전달된 점수만큼 증가
        UpdateScoreUI();
        if (currentScore >= 3000 && !isBossStageStarted)
        {
            StartBossStage();
        }
    }
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + currentScore;
        }
    }
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ActivateSlowMotion(float duration, float amount)
    {
        StopAllCoroutines(); // 이전 슬로우 효과 중첩 방지
        StartCoroutine(SlowRoutine(duration, amount));
    }

    IEnumerator SlowRoutine(float duration, float amount)
    {
        globalSpeedMultiplier = amount;
        

        yield return new WaitForSeconds(duration);

        globalSpeedMultiplier = 1.0f;
       
    }

    void ApplySpeedToAllEnemies(float factor)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies) { e.speed *= factor; }

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb b in bombs)
        {
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();

            if (rb != null) rb.gravityScale *= (factor * factor);
        }

        
        B2Bomber[] bombers = FindObjectsOfType<B2Bomber>();
        foreach (B2Bomber b in bombers) { b.speed *= factor; }
    }
    void Start()
    {

        Time.timeScale = 1f;
        globalSpeedMultiplier = 1.0f;
    }

  
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        globalSpeedMultiplier = 0f;
        Time.timeScale = 0f;
        Debug.Log("게임 오버!");
    }

}
