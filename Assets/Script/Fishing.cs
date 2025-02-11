using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

    ///<summary>
    ///Fishing类 and fishing mechnaic
    ///Kehan Gong
    ///2025-02-08
    ///</summary>   
    ///所有种类的稀有度总和必须为100%

public enum Rarity {Common, Rare, Legend}

  [System.Serializable]
    public class Fish
    {
        public Rarity rarity;
        public string fishName;
        public int value;
        public GameObject fishObject;

        ///<summary>
        ///Define the fish name, rarity, probability, value, and price
        ///</summary>

    }

public class Fishing : MonoBehaviour
{
    /// <summary>
    /// Define the rarity setting and the probability increase设置概率
    /// </summary>
    [System.Serializable]
    public class RaritySetting
    {
    public Rarity rarity;
    public List<Fish> fishs = new List <Fish>();
    [Range(0,100)] public float probability;

    [HideInInspector] public float currentProbability;
    }
   [SerializeField] private List<RaritySetting> raritySettings = new List<RaritySetting>();
   [SerializeField] private float legendProbabilityIncrease = 1f; 
   [SerializeField] private float cooldownTime = 1.5f; // 冷却时间
   [SerializeField] private float lastDrawTime = -Mathf.Infinity; // 上次抽卡时间


   ///<summary>
   ///Define the rarity setting and the probability increase设置概率
   ///</summary>

   public void Start()
   {
        ///初始化概率   
         foreach (var setting in raritySettings)
        {
            setting.currentProbability = setting.probability;
        }
        //Normalize the probability
   }
   /*
   public void Update()
   {
    if (Input.GetKeyDown(KeyCode.E))
    {
        DrawFish();
       
    }
   }
*/   

    public Fish DrawFish()
    {
    /// <summary>
    /// Draw probability to each fish.
    /// 为每种鱼设置概率。
    /// </summary>
     if (Time.time < lastDrawTime + cooldownTime)
    {
        Debug.Log("冷却中，请稍后再试！");
        return null;
    }

    // 记录本次抽卡时间
    lastDrawTime = Time.time;
    // Calculate the total probability of all rarities.
    // 计算所有稀有度的总概率。
    float total = raritySettings.Sum(r => r.currentProbability);
    Debug.Log("total: " + total);
    // Generate a random number between 0 and the total probability.
    // 生成一个 0 到总概率之间的随机数。
    float random = Random.Range(0f, total);

    // Initialize a variable to accumulate the probability.
    // 初始化一个变量来累加概率。
    float currentProbability = 0f;

    /// Select the fish based on probability.
    /// 根据概率选择鱼。
    RaritySetting selectedRarity = null;
    foreach (var setting in raritySettings)
    {
        // Add the current rarity's probability to the accumulator.
        // 将当前稀有度的概率加到累加器中。
        currentProbability += setting.probability;
        Debug.Log("currentProbability: " + currentProbability);

        // If the random number falls within the current probability range, select this rarity.
        // 如果随机数落在当前概率范围内，选择这个稀有度。
        if (random <= currentProbability)
        {
            selectedRarity = setting;
            break;
        }
    }

    // Randomly select a fish from the selected rarity's fish list.
    // 从选中的稀有度的鱼列表中随机选择一条鱼。
    Fish selectedFish = selectedRarity.fishs[Random.Range(0, selectedRarity.fishs.Count)];

    // If the selected fish is not legendary, adjust the legend probability.
    // 如果选中的鱼不是传说鱼，调整传说鱼的概率。
    if (selectedRarity.rarity != Rarity.Legend)
    {
        AdjustLengendProbability();
    }
    else
    {
        // If the selected fish is legendary, reset all probabilities.
        // 如果选中的鱼是传说鱼，重置所有概率。
        ResetProbabilities();
    }

    // Return the selected fish.
    // 返回选中的鱼。
    return selectedFish;

    }

    ///<summary>
    ///Adjust the probability of the legend fish
    ///</summary>
    private void AdjustLengendProbability()
    {
        ///找到传奇概率设置和非传奇概率设置
        ///find the legend probability setting and non legend probability setting
        var legendSetting = raritySettings.Find(r => r.rarity == Rarity.Legend);
        var nonLegendSettings = raritySettings.Where(r => r.rarity != Rarity.Legend).ToList();
        
        ///计算新的概率
        ///calculate the new probability
        
        float newLegendProb = legendSetting.probability + legendProbabilityIncrease;
        ///make sure the probability is not greater than 100% or lower than 0%
        ///确保概率不超过100%
        newLegendProb = Mathf.Clamp(newLegendProb, 0f, 100f);

        ///计算重新计算的概率
        ///calculate the probability difference        

        float delta = newLegendProb - legendSetting.probability;
        if(delta <= 0)return;
        
        ///计算非传奇概率设置的总概率
        ///calculate the total probability of non legend probability setting

        float totalNonLegend = nonLegendSettings.Sum(r => r.probability);
        if(totalNonLegend <= 0 )return;
        
        ///计算每个非传奇概率设置的新概率
        ///calculate the new probability of each non legend probability setting
        ///确保概率不低于0%
        ///make sure the probability is not lower than 0%
        foreach (var setting in nonLegendSettings)
        {
            float proportion = setting.probability/totalNonLegend;
            setting.probability -= delta * proportion;
            setting.probability = Mathf.Max(setting.probability,0);
        }
        legendSetting.probability = newLegendProb;
    }
    private void ResetProbabilities()
    {
        foreach (var setting in raritySettings)
        {
            setting.probability = setting.currentProbability;
        }
        NormalizeProbabilities(); // 确保概率总和为100%
    }

    /// 归一化概率，确保所有稀有度的概率总和为100%
    
    private void NormalizeProbabilities()
    {
        float total = raritySettings.Sum(r => r.probability);
        if (Mathf.Abs(total - 100) > 0.01f)
        {
            Debug.LogWarning("Probabilities not normalized! Total: " + total);
        }
    }
}


