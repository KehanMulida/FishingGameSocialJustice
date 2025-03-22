using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject store_UI;
    public Button store_button;

    void Start()
    {
        store_button.onClick.AddListener(onbuttonClick); // 定义监听事件
        store_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onbuttonClick()
    {
        if (store_UI.activeInHierarchy)
        {
            store_UI.SetActive(false);
        }

        else
        {
            store_UI.SetActive(true);
        }
    }
}
