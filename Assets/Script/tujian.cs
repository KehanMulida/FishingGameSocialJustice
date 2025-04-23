using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaUIController : MonoBehaviour
{
    public static EncyclopediaUIController instance;

    [Header("UI 引用")]
    public Button openButton;           // 主界面 “图鉴” 按钮
    public GameObject encyclopediaPanel; // 图鉴主面板
    public Button closeButton;          // 关闭按钮

    [Header("左侧 Icon 列表")]
    public Transform iconGridParent;    // IconGrid 的 Transform
    public GameObject iconButtonPrefab; // IconButton 预制件

    [Header("右侧详情区")]
    public Image detailImage;           // 高亮显示的鱼大图
    public Text detailName;             // 鱼名称
    public Text detailBackground;       // （可选）图鉴背景说明

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        openButton.onClick.AddListener(OpenEncyclopedia);
        closeButton.onClick.AddListener(CloseEncyclopedia);
        CloseEncyclopedia();
    }

    public void OpenEncyclopedia()
    {
        RefreshIconList();
        encyclopediaPanel.SetActive(true);

        var keys = new List<string>(Inventory.instance.fishData.Keys);
        if (keys.Count > 0)
        {
            ShowDetail(keys[0]);
        }
    }

    private void CloseEncyclopedia()
    {
        encyclopediaPanel.SetActive(false);
    }

    private void RefreshIconList()
    {
        foreach (Transform t in iconGridParent)
        {
            Destroy(t.gameObject);
        }

        var inv = Inventory.instance;

        foreach (var kvp in inv.fishData)
        {
            string fishName = kvp.Key;
            string capturedName = fishName; // 避免闭包问题

            Sprite iconSp = inv.fishSprites.ContainsKey(fishName)
                ? inv.fishSprites[fishName]
                : null;

            GameObject go = Instantiate(iconButtonPrefab, iconGridParent);
            go.GetComponent<Image>().sprite = iconSp;

            Button btn = go.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            
            btn.onClick.AddListener(() =>
            {
                Debug.Log("点击图鉴按钮，展示鱼：" + capturedName);
                ShowDetail(capturedName);
            });
        }
    }

    public void ShowDetail(string fishName)
    {
        Debug.Log("调用 ShowDetail：" + fishName);
        var pair = Inventory.instance.fishIconPairs
                         .Find(p => p.fishName == fishName);

        if (pair != null)
        {
            detailImage.sprite = pair.fishSprite;
            detailName.text = pair.fishName;
            detailBackground.text = pair.backgroundInfo;
        }
        else
        {
            detailImage.sprite = null;
            detailName.text = fishName;
            detailBackground.text = "";
        }
    }

    
} 