using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
   public Fishing currentActivePool; // 当前激活的卡池

    // 切换卡池（直接赋值方式）
    public void SwitchPool(Fishing newPool)
    {
        currentActivePool = newPool;
        Debug.Log($"切换到卡池: {newPool?.name ?? "无"}");
    }
}
