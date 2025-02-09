using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    public Fishing fishing;
    public int Money;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CompleteTask(); // 按下空格键触发抽卡
        }
    }   
    
    public void CompleteTask()
    {
        Fish fish = fishing.DrawFish();

        if(fish !=null)
        {
        Debug.Log("DrawFish");
        GivenValue(fish);
        }
        else
        {
            Debug.Log("No fish");
        }
       
    }
    public void GivenValue(Fish fish)

    {
        Money += fish.value;
        string fishName = fish.fishName;
        Debug.Log("Money: " + Money);
        Debug.Log("fishName: " + fishName);
    }

}
