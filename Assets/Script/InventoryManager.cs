using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();  // 存储物品列表
    public GameObject inventoryPanel;  // 背包 UI 面板
    public Transform itemGrid;  // 物品格子父级（ItemGrid）
    public GameObject itemSlotPrefab;  // 物品槽预制体
    public int maxSlots = 42; // 设定最大背包格子数

    private List<GameObject> slots = new List<GameObject>(); // 存储所有生成的 ItemSlot

    void Start()
    {
        GenerateInventorySlots(); // 预生成所有空格子
        UpdateInventoryUI();
    }

    void GenerateInventorySlots()
    {
        // **先清空所有旧的物品槽**
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }
        slots.Clear(); // 清除列表中的旧物品槽

        // **创建固定数量的空物品槽**
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
            slots.Add(slot); // 记录到列表中
        }
    }

    public void AddItem(Item newItem)
    {
        if (inventory.Count >= maxSlots) return; // **如果背包满了，不添加**

        inventory.Add(newItem);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        // **遍历所有的格子，显示物品**
        for (int i = 0; i < slots.Count; i++)
        {
            Image itemImage = slots[i].transform.GetChild(0).GetComponent<Image>(); // 获取 ItemIcon

            if (i < inventory.Count)
            {
                // **有物品的格子，显示物品图标**
                itemImage.sprite = inventory[i].itemIcon;
                itemImage.enabled = true;
            }
            else
            {
                // **没有物品的格子，隐藏图标**
                itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }
    }
}
