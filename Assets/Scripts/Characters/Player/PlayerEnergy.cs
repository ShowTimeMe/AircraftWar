using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 能量值系统
/// </summary>
public class PlayerEnergy : Singleton<PlayerEnergy> //单例
{
    [SerializeField] EnergyBar energyBar;
    public  const int Max = 100;//能量值
    public  const int PERCENT = 1;//百分比值
    int energy;//存储当前能量值


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
        energy = Mathf.Clamp(energy + value, 0, Max);//能量数值限制
        energyBar.UpdateStats(energy, Max);//更新UI显示
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
