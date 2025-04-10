using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    // 请在 Inspector 中将你的背包面板赋值到该字段
    public GameObject inventoryPanel;

    private void Start()
    {
        // 可选：一开始隐藏背包界面
        if(inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }
    
    private void Update()
    {
        // 按下“B”键时切换背包的显示状态
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    /// <summary>
    /// 切换 InventoryPanel 的激活状态
    /// </summary>
    public void ToggleInventory()
    {
        if(inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }
}
