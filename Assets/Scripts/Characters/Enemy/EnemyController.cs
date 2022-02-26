using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˿������ű�
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("�Ӵ�XY")]
    [SerializeField] float paddingX;
    [SerializeField] float paddingY;//�Ӵ�Y
    [Header("�ƶ��ٶ�")]
    [SerializeField] float moveSpeed = 2f;//�ƶ��ٶ�

    [Header("��ת�Ƕ�")]
    [SerializeField] float moveRotationAngle = 25f;


    [Header("������ʱ�������Сֵ")]
    [SerializeField] float minFireInterval;
    [SerializeField] float maxFireInterval;
    [Header("�����ӵ�Ԥ����")]
    [SerializeField] GameObject[] projectiles;
    [Header("����")]
    [SerializeField] AudioData[] projectileLaunchSFX;
    [Header("�����ӵ�����λ��")]
    [SerializeField] Transform muzzle;


    void OnEnable()
    {
        StartCoroutine(nameof(RandomlyOmveingCoroutine));
        StartCoroutine(nameof(RandomlyFireCoroutine));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();//ͣ������Э��
    }
    /// <summary>
    /// ����ƶ�Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomlyOmveingCoroutine()
    {
        transform.position = ViewPort.Instance.RandomEnemySpawnPosition(paddingX, paddingY);
        Vector3 targetPosition = ViewPort.Instance.RandomRightHalfPosition(paddingX, paddingY);
        while (gameObject.activeSelf)
        {
            //������û�е���ָ��λ��(Mathf.Epsilon������0�ļ�С��)
            if (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
            {
                //����ǰ��Ŀ��λ��
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                //������ת
                transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).normalized.y * moveRotationAngle, Vector3.right);
            }
            else//����ָ��λ��
            {
                //����һ���µ�λ��
                targetPosition = ViewPort.Instance.RandomRightHalfPosition(paddingX, paddingY);
            }
            yield return null;
        }
    }

    /// <summary>
    /// �����������Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomlyFireCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));
            foreach (var projectile in projectiles)
            {
                PoolManager.Release(projectile, muzzle.position);
            }
            AudioManager.Instance.PlayRandomSFX(projectileLaunchSFX);
        }
    }
}
