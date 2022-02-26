using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人控制器脚本
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("视窗XY")]
    [SerializeField] float paddingX;
    [SerializeField] float paddingY;//视窗Y
    [Header("移动速度")]
    [SerializeField] float moveSpeed = 2f;//移动速度

    [Header("旋转角度")]
    [SerializeField] float moveRotationAngle = 25f;


    [Header("开火间隔时间最大最小值")]
    [SerializeField] float minFireInterval;
    [SerializeField] float maxFireInterval;
    [Header("敌人子弹预制体")]
    [SerializeField] GameObject[] projectiles;
    [Header("声音")]
    [SerializeField] AudioData[] projectileLaunchSFX;
    [Header("敌人子弹生成位置")]
    [SerializeField] Transform muzzle;


    void OnEnable()
    {
        StartCoroutine(nameof(RandomlyOmveingCoroutine));
        StartCoroutine(nameof(RandomlyFireCoroutine));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();//停用所有协程
    }
    /// <summary>
    /// 随机移动协程
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomlyOmveingCoroutine()
    {
        transform.position = ViewPort.Instance.RandomEnemySpawnPosition(paddingX, paddingY);
        Vector3 targetPosition = ViewPort.Instance.RandomRightHalfPosition(paddingX, paddingY);
        while (gameObject.activeSelf)
        {
            //如果玩家没有到达指定位置(Mathf.Epsilon不等于0的极小数)
            if (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
            {
                //继续前往目标位置
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                //敌人旋转
                transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).normalized.y * moveRotationAngle, Vector3.right);
            }
            else//到达指定位置
            {
                //生成一个新的位置
                targetPosition = ViewPort.Instance.RandomRightHalfPosition(paddingX, paddingY);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 敌人随机开火协程
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
