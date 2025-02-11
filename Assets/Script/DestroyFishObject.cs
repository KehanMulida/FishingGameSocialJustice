using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFishObject : MonoBehaviour
{
    ///<summary>
    ///Destroy the fish object when it collides with the trigger    
    ///Kehan Gong
    ///2025-02-10
    ///</summary>
   

    void Start()
    {
        Destroy(gameObject, 3f); // 3秒后自动销毁
    }
}
