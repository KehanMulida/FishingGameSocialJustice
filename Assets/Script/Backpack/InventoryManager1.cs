using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public Dictionary<ItemType, ItemData> itemDataDic = new Dictionary<ItemType, ItemData>();

    public InventoryData backpack;
    public InventoryData toolbarData;

    public void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Data");
        foreach (ItemData data in itemDataArray)
        {
            itemDataDic.Add(data.type, data);
        }

        backpack = Resources.Load<InventoryData>("Data/backpack");
        toolbarData = Resources.Load<InventoryData>("Data/Toolbar");

    }

    public ItemData GetDataValue(ItemType type)
    {
        ItemData data;
        bool isSuccess = itemDataDic.TryGetValue(type, out data);
        if (isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("你传递的type" + type + "不存在");
            return null;
        }

    }

    public void AddToBag(ItemType type)
    {
        ItemData item = GetDataValue(type);
        if (item == null)
        {
            return;
        }

        foreach (SlotData slotdata in backpack.slotList)
        {
            if (slotdata.item == item && slotdata.CanAddItem())
            {
                slotdata.AddOne();
                return;
            
            }
           
        }
        foreach (SlotData slotdata in backpack.slotList)
        {
            if (slotdata.count == 0)
            {
                slotdata.AddItem(item);
                return;

            }

        }
        Debug.LogWarning("你的背包已满");

    }

}
