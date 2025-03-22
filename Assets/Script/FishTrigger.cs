using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    public Fishing fishing;
    //public int Money;
    private GameObject fishObject;
    public Transform player;
    public float spawnDistance = 2f;
    
    private int money;
    public int Money
    {
        get { return money; }
        private set
        {
            money = value;
            if (coinManager != null)
            {
                coinManager.UpdateMoneyUI(money); // 更新 UI
            }
        }
    }

    public CoinManager coinManager; // 参考 CoinUIManager

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CompleteTask(); // 按下空格键触发抽卡
        }
    }   

    private void Start()
    {
        Money = 0;
    }
    
    public void CompleteTask()
    {
        Fish fish = fishing.DrawFish();

        if(fish !=null)
        {
        Debug.Log("DrawFish");
        GivenValue(fish);
        SpawnFish(fish);
        }
        else
        {
            Debug.Log("No fish");
        }
       
    }
    public void GivenValue(Fish fish)

    {
        Money += fish.value;
        string fishName = fish.fishName;
        Debug.Log("Money: " + Money);
        Debug.Log("fishName: " + fishName);
    }

    public void SpawnFish(Fish fish)
    {
        if (fish.fishObject == null || player == null)
        {
            Debug.LogWarning("Fish Prefab 或 Player 未设置！");
            return;
        }

        // 计算生成位置（玩家附近）
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;

        // 实例化卡牌的 GameObject
        GameObject fishObjectInstance = Instantiate(fish.fishObject, spawnPosition, Quaternion.identity);
    }

    public void upgrade_speed()
    {
        money -= 100;
    }

}
