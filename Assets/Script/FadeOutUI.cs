using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 引入 TextMeshPro 命名空间

public class FadeOutUI : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup canvasGroup;  // 用来控制整个 Canvas 透明度
    public TMP_Text text;     // 用来控制文字的透明度
    public float fadeDuration = 4f;  // 淡出时间

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // 透明度逐渐减少
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);

            // 逐渐淡出 Canvas 和 Text
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }

            if (text != null)
            {
                Color textColor = text.color;
                text.color = new Color(textColor.r, textColor.g, textColor.b, alpha); // 控制文字透明度
            }

            yield return null; // 每帧更新一次
        }

        // 确保完全透明
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        if (text != null)
        {
            Color textColor = text.color;
            text.color = new Color(textColor.r, textColor.g, textColor.b, 0f); // 确保文字透明
        }
    }
}
