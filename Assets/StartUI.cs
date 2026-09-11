using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour
{
    public RectTransform StartText;

    void Start()
    {
        StartCoroutine(PlayStartAnimation());
    }

    IEnumerator PlayStartAnimation()
    {
        // 시작 위치
        StartText.anchoredPosition = new Vector2(-2000, 0);

        float timer = 0f;
        float duration = 2f;

        Vector2 startPos = new Vector2(-2000, 0);
        Vector2 endPos = new Vector2(2000, 0);

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            StartText.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        StartText.gameObject.SetActive(false);
    }
}