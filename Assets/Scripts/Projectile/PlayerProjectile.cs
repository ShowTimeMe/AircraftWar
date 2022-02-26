using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ӵ�Ԥ����
/// </summary>
public class PlayerProjectile :Projectile
{
    //�켣���
    TrailRenderer trail;

    private void Awake()
    {
        if (moveDirection != Vector2.right)
        {
            //-------------���ݿ�ʼ�ͽ�������һ����תֵ------��ʼ����--------��������
            transform.GetChild(0).rotation = Quaternion.FromToRotation(Vector2.right, moveDirection);
        }
        trail = GetComponentInChildren<TrailRenderer>();//��ȡ�켣�����ֵ
    }
    /// <summary>
    /// ����ӵ�������ʱ�����ӵ��켣
    /// </summary>
    private void OnDisable()
    {
        trail.Clear();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        PlayerEnergy.Instance.Obtain(PlayerEnergy.PERCENT);//����ӵ����е�����������ֵ
    }
}
