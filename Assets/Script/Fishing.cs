using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

    ///<summary>
    ///Fishing类 and fishing mechnaic
    ///Kehan Gong
    ///2025-02-08
    ///</summary>   

public enum Rarity {Common, Rare, Legend}

  [System.Serializable]
    public class Fish
    {
        public Rarity rarity;
        public string fishName;
        public int value;

        ///<summary>
        ///Define the fish name, rarity, probability, value, and price
        ///</summary>

    }

public class Fishing : MonoBehaviour
{
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

    public Fish DrawFish()
    {
        ///<summary>  
        ///Draw probability to each fish 为每个鱼设置概率
        ///</summary>

        float total = raritySettings.Sum(r => r.currentProbability);
        float random = Random.Range(0f, total);
        float currentProbability = 0f;

        ///选择鱼
        RaritySetting selectedRarity = null;
        foreach (var setting in raritySettings)
        {
            currentProbability += setting.probability;
            if (random <= currentProbability)
            {
                selectedRarity = setting;
                break;
            }
        }
        
        Fish selectedFish = selectedRarity.fishs[Random.Range(0, selectedRarity.fishs.Count)];

        if(selectedRarity.rarity != Rarity.Legend)
        {
            AdjustLengendProbability();
        }
        else
        {
            ResetProbabilities();
        }

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


