using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    [Header("UI Components")]
    public Image icon;           // 槽位图片
    public Text quantityText;    // 显示数量

    [Header("默认图案")]
    public Sprite defaultSprite; // 背包空时显示的默认图

    /// <summary>
    /// 用于更新该格子的内容
    /// </summary>
    /// <param name="sprite">鱼的Sprite</param>
    /// <param name="quantity">数量</param>
    public void SetSlot(Sprite sprite, int quantity)
    {
        icon.sprite = sprite;
        icon.enabled = true;

        // 如果数量>0，就显示，反之隐藏
        if (quantity > 0)
        {
            quantityText.text = quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 清空该格子，恢复默认背景
    /// </summary>
    public void ClearSlot()
    {
        // 恢复默认图标
        icon.sprite = defaultSprite;
        icon.enabled = true;

        // 隐藏数量文字
        quantityText.gameObject.SetActive(false);
    }
}
