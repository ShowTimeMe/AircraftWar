using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����
/// </summary>
[System.Serializable]//δ�̳�ʹ�� ��¶��Pool�е����л��ֶ�
public class Pool 
{
    //���Ա��ʽ
    /*
    public GameObject Prefab
    {
        get { return prefab; }
    }
    */


    //���Ա��ʽ
    public GameObject Prefab => prefab;

    /// <summary>
    /// ����ش�С
    /// </summary>
    public int Size => size;
    /// <summary>
    /// �����ʵ�����гߴ磨��������ܸ��������е�Count����
    /// </summary>
    public int RuntimeSize => queue.Count;

    //����
    [SerializeField]GameObject prefab;
    [SerializeField]int size = 1;

    //����
    Queue<GameObject> queue;

    Transform parent;//����λ��

    public  void Initialize(Transform parent)
    {
        //��ֵ
        queue = new Queue<GameObject>();
        this.parent = parent;
        //ѭ������
        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());//��������
        }
    }


   //��������-����ʾ-������
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }


    /// <summary>
    /// ���ö���
    /// </summary>
    /// <returns></returns>
    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        //---------------------------���еĵ�һ��Ԫ��
        if (queue.Count > 0&&!queue.Peek().activeSelf)
        {
            //���ó���Ԫ��
            availableObject = queue.Dequeue();
        }
        else//�޿��ö���-����
        {
            availableObject = Copy();
        }

        //���С������ض����
        queue.Enqueue(availableObject);
        return availableObject;//���ؿ��ö���
    }

    /// <summary>
    /// ���ÿ��ö���
    /// </summary>
    /// <returns></returns>
    public  GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    /// <summary>
    /// ����
    /// λ��
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
    /// ����
    /// λ��
    /// ��ת
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
    /// ����
    /// λ��
    /// ��ת
    /// ��ά��������
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
