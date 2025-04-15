using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 如果使用的是 TextMeshPro

public class CoinManager : MonoBehaviour
{
    ///<summary>
    ///Manage the coin
    ///Kehan Gong
    ///2025-02-10
    ///</summary>
    /*
    public TextMeshProUGUI moneyText; // 用于显示金币的 TextMeshPro 组件

    // 更新 UI 中的 Money 值
    public void UpdateMoneyUI(int money)
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money;
        }
    }
   */
    public TextMeshProUGUI coinText;
    private int coinCount = 0;
    public FishTrigger fishTrigger;
    public float animationDuration = 0.5f;
    public float scrollSpeed = 10f;

    private Vector3 originalPosition; // 👈 缓存起始位置

    void Start()
    {
        // 缓存初始位置
        originalPosition = coinText.transform.localPosition;
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        amount = fishTrigger.Money - coinCount;
        coinCount += amount;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        // 确保位置复位
        coinText.transform.localPosition = originalPosition;
        StartCoroutine(AnimateCoinUI());
    }

    IEnumerator AnimateCoinUI()
    {
        int startValue = int.Parse(coinText.text.Replace("Coins: ", ""));
        int targetValue = coinCount;
        float elapsed = 0f;

        Vector3 startPos = originalPosition;
        Vector3 targetPos = startPos + Vector3.up * 50f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / animationDuration;

            int displayValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, progress));
            coinText.text = "Coins: " + displayValue;

            if (progress < 0.5f)
            {
                coinText.transform.localPosition = Vector3.Lerp(targetPos, startPos, progress * 2f);
            }
            else
            {
                coinText.transform.localPosition = startPos;
            }

            yield return null;
        }

        // 最终强制位置归位
        coinText.text = "Coins: " + targetValue;
        coinText.transform.localPosition = startPos;
    }
    
}
