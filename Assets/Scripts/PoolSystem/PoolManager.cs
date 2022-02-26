using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 池管理器
/// </summary>
public class PoolManager : MonoBehaviour
{
    [Header("对象池数组")]
    [SerializeField] Pool[] enemyPools;
    [SerializeField] Pool[] playerProjectilePools;
    [SerializeField] Pool[] enemyProjectilePools;
    [SerializeField] Pool[] vFXPools;

    //字典--对象池字典
    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();//初始化字典
        Initialize(enemyPools);
        Initialize(playerProjectilePools);//调用初始化函数传入玩家子弹池数组
        Initialize(enemyProjectilePools);
        Initialize(vFXPools);

    }
#if UNITY_EDITOR //在Unity测试下使用
    private void OnDestroy()
    {
        //传入玩家子弹对象池数组
        CheckPoolSize(enemyPools);
        CheckPoolSize(playerProjectilePools);
        CheckPoolSize(enemyProjectilePools);
        CheckPoolSize(vFXPools);
    }
#endif

    /// <summary>
    /// 检测对象池运行尺寸的函数
    /// </summary>
    /// <param name="pools"></param>
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool:{0}对象池实际运行尺寸{1}大于初始化尺寸{2}",pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }    
    }

    /// <summary>
    /// 初始化生成预制体
    /// </summary>
    /// <param name="pools"></param>
    private void Initialize(Pool[] pools)
    {
        //遍历
        foreach(var pool in pools)
        {
#if UNITY_EDITOR  //条件编译指令-Unity输出错误语句
            if (dictionary.ContainsKey(pool.Prefab))//如果字典里有预制体的key
            {
                Debug.LogError("预制体错误" + pool.Prefab.name);
                continue;//
            }
#endif
            //给字典添加元素(对象池预制体，对象池)
            dictionary.Add(pool.Prefab, pool);
            //设置父级物体
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;

            pool.Initialize(poolParent);//调用初始化函数来生成预制体
        }

    }
    /// <summary>
    /// 释放一个对象池中预备好的对象
    /// </summary>
    /// <param name="prefab">参数，返回对象池中预备好的游戏对象</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR //条件编译-只在Unity下执行
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();//取得对象池中预备好的预制体
    }
    /// <summary>
    /// 释放一个对象池中预备好的对象
    /// </summary>
    /// <param name="prefab">返回对象池中预备好的游戏对象</param>
    /// <param name="position">指定释放的位置</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR //条件编译-只在Unity下执行
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);//取得对象池中预备好的预制体
    }
    /// <summary>
    /// 释放一个对象池中预备好的对象
    /// </summary>
    /// <param name="prefab">返回对象池中预备好的游戏对象</param>
    /// <param name="position">指定释放的位置</param>
    /// <param name="rotation">旋转角度</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR //条件编译-只在Unity下执行
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation);//取得对象池中预备好的预制体
    }

    /// <summary>
    /// 释放一个对象池中预备好的对象
    /// </summary>
    /// <param name="prefab">返回对象池中预备好的游戏对象</param>
    /// <param name="position">指定释放的位置</param>
    /// <param name="rotation">旋转角度</param>
    /// <param name="localScale">预制体三维缩放值</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector3 position,Quaternion  rotation,Vector3 localScale)
    {
#if UNITY_EDITOR //条件编译-只在Unity下执行
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到预制体：" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation, localScale);//取得对象池中预备好的预制体
    }
}
