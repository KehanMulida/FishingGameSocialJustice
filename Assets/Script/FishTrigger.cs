using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                coinManager.AddCoins(money); // 更新 UI
            }
        }
    }

    public CoinManager coinManager; // 参考 CoinUIManager

    private void Update()
    {
        fishing = FishPoolManager.Instance.currentPool;
        if (Input.GetKeyDown(KeyCode.E))
        {
            CompleteTask(); // 按下E键触发抽卡
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

        // ==> 加入背包
        if (Inventory.instance != null)
        {
            Inventory.instance.AddFish(fishName);
        }
        else
        {
            Debug.LogWarning("Inventory 实例不存在！");
        }
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
        if(Money >= 100)
        {
            Money -= 100;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void getCapturedbyNPC()
    {
        Money -= 100;
        if(Money < 0)
        {
            Invoke("RestartGame", 1f);
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
