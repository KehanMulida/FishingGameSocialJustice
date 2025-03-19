using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // 背包UI面板

    void Start()
    {
        inventoryPanel.SetActive(false); // 初始状态隐藏
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf); // 切换可见性
    }
}