using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class boatSpeed_Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    private FishTrigger money; // get the boat script
    private PlayerMovement movement;
    public GameObject store_UI;
    public Button speed_upgrade01;
    private bool canUpgrade;
    public TextMeshProUGUI upgrade_fail_text;
    
   
   
    void Start()
    {
        money = FindObjectOfType<FishTrigger>();
        movement = FindObjectOfType<PlayerMovement>();
        speed_upgrade01.onClick.AddListener(onbuttonClick); // 定义监听事件
        canUpgrade = false;
        upgrade_fail_text.text = "Not enough coins";
        upgrade_fail_text.gameObject.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        upgradeCheck();
       
    }

    public void onbuttonClick() // increase boat speed
    {
       if (canUpgrade)
        {
            movement.moveSpeed += 1;
            money.upgrade_speed();
        }

       else
        {
            Debug.Log("not enough coins");
            upgrade_fail_text.gameObject.SetActive(true);
        }

      
    }

    public void upgradeCheck()
    {
        if (money.Money >= 100)
        {
            canUpgrade = true;
        }
    }

   
}
