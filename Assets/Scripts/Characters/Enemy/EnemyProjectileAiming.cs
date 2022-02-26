using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��׼��ҵ��ӵ�
/// </summary>
public class EnemyProjectileAiming : Projectile
{
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void OnEnable()
    {
        StartCoroutine(nameof(MoveDirectionCoroutine));
        base.OnEnable();
    }

    /// <summary>
    /// �ȴ��ƶ�Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveDirectionCoroutine()
    {
        yield return null;
        if(target.activeSelf)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
        }
    }

}
