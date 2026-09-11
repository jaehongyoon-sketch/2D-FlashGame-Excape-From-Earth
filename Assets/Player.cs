using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class Player : MonoBehaviour
{
    public int maxJumpCount = 2;
    int currentJumpCount = 0;
    public float moveSpeed = 6f;
    public float attackRange = 3.0f;
    public bool hasShield = false;
    public GameObject shieldVisual;
    public GameObject attackVisual;
    public float scoreTextOffset = 1.0f;
    public float scoreTextDuration = 1.0f; 

    public float attackDuration = 0.1f;

    public float jumpForce = 15f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            return;
        }
        Move();

        if (Input.GetKeyDown(KeyCode.K)) Attack(-1);
        if (Input.GetKeyDown(KeyCode.L)) Attack(1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentJumpCount < maxJumpCount) Jump();
        }
    }
    

    // --- 충돌 감지 (물리적 충돌) ---
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("Laser") ||
            collision.gameObject.CompareTag("Bomb"))
        {
            HandleDamage(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            currentJumpCount = 0;
        }
    }

    // --- 트리거 감지 (레이저, 폭탄 등 통과하는 물체) ---
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") ||
            other.CompareTag("Laser") ||
            other.CompareTag("Bomb"))
        {
            HandleDamage(other.gameObject);
        }
    }

    // --- 데미지 처리 핵심 로직 ---
    void HandleDamage(GameObject attacker)
    {
        if (hasShield)
        {
            hasShield = false;
            if (shieldVisual != null) shieldVisual.SetActive(false);

            Debug.Log($"보호막 작동: {attacker.tag} 방어!");

            // 재충돌 방지 및 파괴
            attacker.tag = "Untagged";
            Destroy(attacker);
        }
        else
        {
            Debug.Log($"{attacker.tag}에 맞음: 게임 오버");
            GameManager.instance.GameOver();
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
        if (shieldVisual != null) shieldVisual.SetActive(true);
        Debug.Log("보호막 활성화 완료!");
    }

    // --- 기타 함수들 (점프, 이동, 공격) ---
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        currentJumpCount++;
    }

    void Move()
    {
        float move = 0;
        if (Input.GetKey(KeyCode.A)) move = -1;
        if (Input.GetKey(KeyCode.D)) move = 1;
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
    }

    void Attack(int dir)
    {
        StartCoroutine(ShowAttackMotion(dir));
            
        Enemy target = FindClosestEnemy(dir);
        if (target != null && Mathf.Abs(target.transform.position.x - transform.position.x) < attackRange)
        {
            Destroy(target.gameObject);
            GameManager.instance.AddScore(target.scoreValue);
            GameObject scoreText = Instantiate(target.scoreTextPrefab, target.transform.position + Vector3.up * scoreTextOffset, Quaternion.identity);

            
            TMPro.TextMeshProUGUI textMeshUI = scoreText.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textMeshUI != null)
            {
                
                textMeshUI.text = "+" + GameManager.instance.GetCurrentScore().ToString();
            }
        
            StartCoroutine(ShowScoreTextRoutine(scoreText));
          
        }
        else
        {
            Boss boss = FindObjectOfType<Boss>();
            if (boss != null && Mathf.Abs(boss.transform.position.x - transform.position.x) < attackRange)
            {
                
                boss.TakeDamage();
            }
            else
            {
                
                GameManager.instance.SubtractScore();
                Debug.Log("Miss! 점수 차감");
            }
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.right * attackRange * dir, Color.red, 0.2f);
    }
    IEnumerator ShowScoreTextRoutine(GameObject scoreText)
    {
        
        TMPro.TextMeshProUGUI textMesh = scoreText.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        float timer = 0f;
        while (timer < scoreTextDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / scoreTextDuration;

            
            scoreText.transform.Translate(Vector3.up * Time.deltaTime * scoreTextOffset);

            
            if (textMesh != null)
            {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f - progress);
            }

            yield return null;
        }

        
        Destroy(scoreText);
    }

    IEnumerator ShowAttackMotion(int dir)
{
        if (attackVisual != null)
        {
            // 1. 초기 세팅 (거리 및 방향)
            float distance = 0.7f;
            attackVisual.transform.localPosition = new Vector3(distance * dir, 0.2f, 0);

            // 이미지 반전 (dir이 -1이면 왼쪽을 보게 함)
            Vector3 currentScale = attackVisual.transform.localScale;
            attackVisual.transform.localScale = new Vector3(Mathf.Abs(currentScale.x) * dir, currentScale.y, currentScale.z);

            // 2. 휘두르기 각도 설정
            // 오른쪽(1)일 때는 45 -> -45도, 왼쪽(-1)일 때는 -45 -> 45도로 계산
            float startAngle = 45f * dir;
            float endAngle = -70f * dir;

            float elapsed = 0f;
            attackVisual.SetActive(true);

            // 3. 정해진 시간(attackDuration) 동안 회전
            while (elapsed < attackDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / attackDuration; // 0에서 1까지 진행도

                // 부드럽게 각도 변경 (Lerp)
                float currentZ = Mathf.Lerp(startAngle, endAngle, progress);
                attackVisual.transform.localRotation = Quaternion.Euler(0, 0, currentZ);

                yield return null; // 다음 프레임까지 대기
            }

            // 4. 마무리
            attackVisual.SetActive(false);
        }
    }

    Enemy FindClosestEnemy(int dir)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closest = null;
        float minDist = Mathf.Infinity;
        foreach (Enemy e in enemies)
        {
            float dx = e.transform.position.x - transform.position.x;
            if ((dir == -1 && dx < 0) || (dir == 1 && dx > 0))
            {
                float dist = Mathf.Abs(dx);
                if (dist < minDist) { minDist = dist; closest = e; }
            }
        }
        return closest;
    }
}
