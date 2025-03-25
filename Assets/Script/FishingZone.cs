using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public string linkedPoolName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FishPoolManager.Instance.RequestSwitchPool(linkedPoolName);
        }
    }
   
}
