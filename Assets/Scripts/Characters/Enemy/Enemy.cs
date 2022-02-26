using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]int deathEnergyBonue = 3;

    public override void Die()
    {
        PlayerEnergy.Instance.Obtain(deathEnergyBonue);//������������һ�� ����ֵ
        EnemyManager.Instance.RemoveFromList(gameObject);
        base.Die();
    }
}
