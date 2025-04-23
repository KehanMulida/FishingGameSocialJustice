using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishIconPair
{
    // 对应鱼的名称（必须与调用 AddFish 时用的字符串一致）
    public string fishName;
    // 对应的鱼图标
    public Sprite fishSprite;

    [TextArea]
    public string backgroundInfo;
}

public class Inventory : MonoBehaviour
{
    // 单例方便其它脚本直接调用 Inventory.instance.AddFish(...)
    public static Inventory instance;

    [Header("背包最大容量")]
    public int maxSlotCount = 16; // 例如 16 格背包

    [Header("所有格子 (在 Inspector 中拖入所有 Slot 对象)")]
    public InventorySlot[] slots;

    // 用来记录背包鱼类信息: key = 鱼名称, value = 数量
    public Dictionary<string, int> fishData = new Dictionary<string, int>();

    [Header("鱼图标映射 (在 Inspector 中添加鱼的映射)")]
    // 你可以在此列表中配置所有鱼的名称及图标（直接拖入对应 Sprite）
    public List<FishIconPair> fishIconPairs = new List<FishIconPair>();

    // 内部使用的字典，通过 fishIconPairs 列表填充（方便查找）
    public Dictionary<string, Sprite> fishSprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // 将 Inspector 中配置的鱼图标映射填充到字典里
        foreach (var pair in fishIconPairs)
        {
            if (!fishSprites.ContainsKey(pair.fishName))
            {
                fishSprites.Add(pair.fishName, pair.fishSprite);
            }
        }

        // 如果需要，可在启动时更新一次 UI
        UpdateInventoryUI();
    }

    /// <summary>
    /// 添加鱼到背包
    /// </summary>
    /// <param name="fishName">鱼名称，必须与鱼图标映射中的 fishName 一致</param>
    public void AddFish(string fishName)
    {
        if (fishData.ContainsKey(fishName))
        {
            fishData[fishName]++;
        }
        else
        {
            fishData.Add(fishName, 1);
        }

        // 数据改动后立刻刷新 UI
        UpdateInventoryUI();
    }

    /// <summary>
    /// 根据背包数据刷新所有 Slot 的显示
    /// </summary>
    public void UpdateInventoryUI()
    {
        // 清空所有 Slot
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }

        // 将鱼的数据逐个写入 Slot，如果超出背包容量就忽略
        int index = 0;
        foreach (var kvp in fishData)
        {
            if (index >= slots.Length)
                break; // 背包满了

            string fishName = kvp.Key;
            int quantity = kvp.Value;

            // 从映射字典里获取对应 Sprite（如果找不到，将使用默认图）
            Sprite fishSprite = null;
            if (fishSprites.ContainsKey(fishName))
            {
                fishSprite = fishSprites[fishName];
            }

            if (fishSprite != null)
            {
                slots[index].SetSlot(fishSprite, quantity);
            }
            else
            {
                // 未找到对应 Sprite，使用 Slot 内设置的默认图
                slots[index].SetSlot(slots[index].defaultSprite, quantity);
            }
            index++;
        }
    }
}
