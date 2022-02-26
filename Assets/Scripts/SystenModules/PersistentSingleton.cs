using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 持久单例
/// 切换场景不被销毁
/// </summary>
public class PersistentSingleton<T> : MonoBehaviour where T:Component
{
   public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else if (Instance!= this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
