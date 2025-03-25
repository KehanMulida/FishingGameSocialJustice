using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public Fishing linkedFishing; // 直接拖拽该区域对应的Fishing脚本
    public FishTrigger trigger;

     private void OnTriggerEnter2D(Collider2D other) // 改为2D版本
    {
        Debug.Log("2D触发进入");
        if(other.CompareTag("Player"))
        {
            if(trigger != null)
            {
                trigger.fishing = linkedFishing;
                Debug.Log($"进入区域: {name}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 改为2D版本
    {
        if(other.CompareTag("Player") && trigger != null)
        {
            trigger.fishing = null;
            Debug.Log($"离开区域: {name}");
        }
    }
}
