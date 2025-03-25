using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
    public static FishPoolManager Instance { get; private set; }

    [Header("卡池配置")]
    [Tooltip("所有可用卡池的注册表")]
    [SerializeField] private List<Fishing> _allPools = new List<Fishing>();
    
    [Header("切换设置")]
    public float switchDelay = 0.2f;
    
    [Header("调试信息")]
    public Fishing currentPool;
    private Coroutine _switchRoutine;
    private Dictionary<string, Fishing> _poolDictionary = new Dictionary<string, Fishing>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        // 初始化卡池字典
        foreach (var pool in _allPools)
        {
            if(pool != null && !_poolDictionary.ContainsKey(pool.name))
            {
                _poolDictionary.Add(pool.name, pool);
            }
        }
    }

    // 通过名称安全获取卡池
    public Fishing GetPoolByName(string poolName)
    {
        _poolDictionary.TryGetValue(poolName, out var pool);
        return pool;
    }

    // 新版区域切换请求
    public void RequestSwitchPool(string poolName)
    {
        if (_switchRoutine != null) 
            StopCoroutine(_switchRoutine);
        
        _switchRoutine = StartCoroutine(SwitchPoolRoutine(poolName));
    }

    private IEnumerator SwitchPoolRoutine(string poolName)
    {
        yield return new WaitForSeconds(switchDelay);
        
        var newPool = GetPoolByName(poolName);
        if (currentPool != newPool)
        {
            currentPool = newPool;
            Debug.Log($"卡池切换: {poolName ?? "无"}");
        }
    }

    // 编辑器辅助：自动收集场景中所有Fishing卡池
    [ContextMenu("收集所有卡池")]
    private void CollectAllPools()
    {
        _allPools = new List<Fishing>(FindObjectsOfType<Fishing>(true));
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
