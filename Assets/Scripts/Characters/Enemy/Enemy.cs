using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]int deathEnergyBonue = 3;

    public override void Die()
    {
        PlayerEnergy.Instance.Obtain(deathEnergyBonue);//敌人死亡后玩家获得 能量值
        EnemyManager.Instance.RemoveFromList(gameObject);
        base.Die();
    }
}
