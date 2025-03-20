using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonToggle : MonoBehaviour
{
    public Image buttonImage; // 按钮的 Image 组件
    public Sprite openSprite; // 打开状态图片
    public Sprite closeSprite; // 关闭状态图片
    private bool isOpen = false; // 追踪状态

    void Start()
    {
        // 确保初始图片正确
        buttonImage.sprite = isOpen ? closeSprite : openSprite;
    }

    public void ToggleInventoryImage()
    {
        isOpen = !isOpen; // 切换状态
        buttonImage.sprite = isOpen ? closeSprite : openSprite;
    }
}
