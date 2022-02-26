using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ع�����
/// </summary>
public class PoolManager : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] Pool[] enemyPools;
    [SerializeField] Pool[] playerProjectilePools;
    [SerializeField] Pool[] enemyProjectilePools;
    [SerializeField] Pool[] vFXPools;

    //�ֵ�--������ֵ�
    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();//��ʼ���ֵ�
        Initialize(enemyPools);
        Initialize(playerProjectilePools);//���ó�ʼ��������������ӵ�������
        Initialize(enemyProjectilePools);
        Initialize(vFXPools);

    }
#if UNITY_EDITOR //��Unity������ʹ��
    private void OnDestroy()
    {
        //��������ӵ����������
        CheckPoolSize(enemyPools);
        CheckPoolSize(playerProjectilePools);
        CheckPoolSize(enemyProjectilePools);
        CheckPoolSize(vFXPools);
    }
#endif

    /// <summary>
    /// ����������гߴ�ĺ���
    /// </summary>
    /// <param name="pools"></param>
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool:{0}�����ʵ�����гߴ�{1}���ڳ�ʼ���ߴ�{2}",pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }    
    }

    /// <summary>
    /// ��ʼ������Ԥ����
    /// </summary>
    /// <param name="pools"></param>
    private void Initialize(Pool[] pools)
    {
        //����
        foreach(var pool in pools)
        {
#if UNITY_EDITOR  //��������ָ��-Unity����������
            if (dictionary.ContainsKey(pool.Prefab))//����ֵ�����Ԥ�����key
            {
                Debug.LogError("Ԥ�������" + pool.Prefab.name);
                continue;//
            }
#endif
            //���ֵ����Ԫ��(�����Ԥ���壬�����)
            dictionary.Add(pool.Prefab, pool);
            //���ø�������
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;

            pool.Initialize(poolParent);//���ó�ʼ������������Ԥ����
        }

    }
    /// <summary>
    /// �ͷ�һ���������Ԥ���õĶ���
    /// </summary>
    /// <param name="prefab">���������ض������Ԥ���õ���Ϸ����</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR //��������-ֻ��Unity��ִ��
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�ع������Ҳ���Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();//ȡ�ö������Ԥ���õ�Ԥ����
    }
    /// <summary>
    /// �ͷ�һ���������Ԥ���õĶ���
    /// </summary>
    /// <param name="prefab">���ض������Ԥ���õ���Ϸ����</param>
    /// <param name="position">ָ���ͷŵ�λ��</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR //��������-ֻ��Unity��ִ��
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�ع������Ҳ���Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);//ȡ�ö������Ԥ���õ�Ԥ����
    }
    /// <summary>
    /// �ͷ�һ���������Ԥ���õĶ���
    /// </summary>
    /// <param name="prefab">���ض������Ԥ���õ���Ϸ����</param>
    /// <param name="position">ָ���ͷŵ�λ��</param>
    /// <param name="rotation">��ת�Ƕ�</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR //��������-ֻ��Unity��ִ��
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�ع������Ҳ���Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation);//ȡ�ö������Ԥ���õ�Ԥ����
    }

    /// <summary>
    /// �ͷ�һ���������Ԥ���õĶ���
    /// </summary>
    /// <param name="prefab">���ض������Ԥ���õ���Ϸ����</param>
    /// <param name="position">ָ���ͷŵ�λ��</param>
    /// <param name="rotation">��ת�Ƕ�</param>
    /// <param name="localScale">Ԥ������ά����ֵ</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector3 position,Quaternion  rotation,Vector3 localScale)
    {
#if UNITY_EDITOR //��������-ֻ��Unity��ִ��
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�ع������Ҳ���Ԥ���壺" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation, localScale);//ȡ�ö������Ԥ���õ�Ԥ����
    }
}
