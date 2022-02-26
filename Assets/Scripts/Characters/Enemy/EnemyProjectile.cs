using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void Awake()
    {
        if (moveDirection != Vector2.left)
        {
            //-------------���ݿ�ʼ�ͽ�������һ����תֵ------��ʼ����--------��������
            transform.rotation = Quaternion.FromToRotation(Vector2.left, moveDirection);
        }
    }
}
