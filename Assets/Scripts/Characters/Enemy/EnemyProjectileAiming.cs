using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 瞄准玩家的子弹
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
    /// 等待移动协程
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
