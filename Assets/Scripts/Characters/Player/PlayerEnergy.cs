using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// ����ֵϵͳ
/// </summary>
public class PlayerEnergy : Singleton<PlayerEnergy> //����
{
    [SerializeField] EnergyBar energyBar;
    public  const int Max = 100;//����ֵ
    public  const int PERCENT = 1;//�ٷֱ�ֵ
    int energy;//�洢��ǰ����ֵ


    private void Start()
    {
        energyBar.InitIalize(energy, Max);
        Obtain(Max);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void Obtain(int value)
    {
        if (energy == Max) return;
        //energy += value;
        energy = Mathf.Clamp(energy + value, 0, Max);//������ֵ����
        energyBar.UpdateStats(energy, Max);//����UI��ʾ
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void Use(int value)
    {
        energy -= value;
        energyBar.UpdateStats(energy, Max);
    }

   /* public bool IsEnough(int value)
    {
        return energy >= value;
    }*/

    public bool IsEnough(int value) => energy >= value;
}
