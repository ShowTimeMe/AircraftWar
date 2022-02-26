using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void Awake()
    {
        if (moveDirection != Vector2.left)
        {
            //-------------根据开始和结束返回一个旋转值------开始方向--------结束方向
            transform.rotation = Quaternion.FromToRotation(Vector2.left, moveDirection);
        }
    }
}
