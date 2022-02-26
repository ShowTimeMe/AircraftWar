using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 子弹预制体
/// </summary>
public class PlayerProjectile :Projectile
{
    //轨迹组件
    TrailRenderer trail;

    private void Awake()
    {
        if (moveDirection != Vector2.right)
        {
            //-------------根据开始和结束返回一个旋转值------开始方向--------结束方向
            transform.GetChild(0).rotation = Quaternion.FromToRotation(Vector2.right, moveDirection);
        }
        trail = GetComponentInChildren<TrailRenderer>();//获取轨迹组件的值
    }
    /// <summary>
    /// 玩家子弹被禁用时消除子弹轨迹
    /// </summary>
    private void OnDisable()
    {
        trail.Clear();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        PlayerEnergy.Instance.Obtain(PlayerEnergy.PERCENT);//玩家子弹命中敌人增加能量值
    }
}
