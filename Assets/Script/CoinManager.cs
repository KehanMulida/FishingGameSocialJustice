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
    public TextMeshProUGUI coinText; // 金币文本
    private int coinCount = 0; // 当前金币数量
    public FishTrigger fishTrigger;

    void Start()
    {
        UpdateCoinUI(); // 初始化 UI
    }

    // 增加金币的方法
    public void AddCoins(int amount)
    {
        amount = fishTrigger.Money-coinCount;
        coinCount += amount;
        UpdateCoinUI(); // 更新 UI，并执行动画
    }

    // 更新金币 UI，并带有平滑增长动画
    void UpdateCoinUI()
    {
        StartCoroutine(AnimateCoinUI());
    }

    IEnumerator AnimateCoinUI()
    {
        int startValue = int.Parse(coinText.text.Replace("Coins: ", ""));
        int targetValue = coinCount;
        float duration = 0.3f; // 动画持续时间
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int displayValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, elapsed / duration));
            coinText.text = "Coins: " + displayValue;
            yield return null;
        }

        coinText.text = "Coins: " + targetValue; // 确保最终数值正确
    }
}
