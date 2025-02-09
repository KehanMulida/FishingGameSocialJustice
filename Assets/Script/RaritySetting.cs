using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaritySetting :ScriptableObject
{
    public Rarity rarity;           // 稀有度
    public List<Fish> fishs = new List<Fish>(); // 该稀有度的卡牌列表
    [Range(0, 100)] public float probability; // 该稀有度的概率
    [HideInInspector] public float currentProbability;
}
