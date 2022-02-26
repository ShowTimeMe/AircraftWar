using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// µ¥Àý½Å±¾
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T>:MonoBehaviour where T:Component
{
    public static T Instance { get; set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }

}
