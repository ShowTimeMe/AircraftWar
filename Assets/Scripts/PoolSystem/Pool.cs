using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对象池
/// </summary>
[System.Serializable]//未继承使用 曝露出Pool中的序列化字段
public class Pool 
{
    //属性表达式
    /*
    public GameObject Prefab
    {
        get { return prefab; }
    }
    */


    //属性表达式
    public GameObject Prefab => prefab;

    /// <summary>
    /// 对象池大小
    /// </summary>
    public int Size => size;
    /// <summary>
    /// 对象池实际运行尺寸（复制体的总个数）队列的Count属性
    /// </summary>
    public int RuntimeSize => queue.Count;

    //对象
    [SerializeField]GameObject prefab;
    [SerializeField]int size = 1;

    //队列
    Queue<GameObject> queue;

    Transform parent;//父级位置

    public  void Initialize(Transform parent)
    {
        //赋值
        queue = new Queue<GameObject>();
        this.parent = parent;
        //循环生成
        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());//加入对象池
        }
    }


   //生成物体-不显示-并返回
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }


    /// <summary>
    /// 可用对象
    /// </summary>
    /// <returns></returns>
    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        //---------------------------队列的第一个元素
        if (queue.Count > 0&&!queue.Peek().activeSelf)
        {
            //调用出列元素
            availableObject = queue.Dequeue();
        }
        else//无可用对象-生成
        {
            availableObject = Copy();
        }

        //入列——返回对象池
        queue.Enqueue(availableObject);
        return availableObject;//返回可用对象
    }

    /// <summary>
    /// 启用可用对象
    /// </summary>
    /// <returns></returns>
    public  GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    /// <summary>
    /// 重载
    /// 位置
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }
    /// <summary>
    /// 重载
    /// 位置
    /// 旋转
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject PreparedObject(Vector3 position,Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }
    /// <summary>
    /// 重载
    /// 位置
    /// 旋转
    /// 三维向量缩放
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject PreparedObject(Vector3 position, Quaternion rotation,Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        return preparedObject;
    }

}
