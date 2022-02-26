using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色类
/// </summary>
public class Character : MonoBehaviour
{
    [Header("角色死亡特效")]
    [SerializeField] GameObject deathVFX;
    [Header("角色死亡爆炸音效")]
    [SerializeField] AudioData deathSFX;
    [Header("角色最大生命值")]
    [SerializeField] protected float maxHealth;
    protected float health;

    [SerializeField] bool showOnHealthBar = true;
    [SerializeField] StatsBar onHeadHealthBar;

    protected virtual void OnEnable()
    {
        health = maxHealth;
        if (showOnHealthBar)
        {
            ShowOnHeadHealthBar();
        }
        else
        {
            HideOnHeadHealthBar();
        }
    }

    /// <summary>
    /// 血条能量条显示
    /// </summary>
    public void ShowOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.InitIalize(health, maxHealth);
    }
    /// <summary>
    /// 血条能量条不显示
    /// </summary>
    public void HideOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// 当角色受伤时
    /// 可被子类重写
    /// </summary>
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if(showOnHealthBar&&gameObject.activeSelf)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 角色死亡
    /// </summary>
    public virtual void Die()
    {
        health = 0f;
        AudioManager.Instance.PlayRandomSFX(deathSFX);
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 角色恢复生命值
    /// </summary>
    public virtual void RestoreHealth(float value)
    {
        if (health == maxHealth) return;
        //health += value;
        //health = Mathf.Clamp(health, 0, maxHealth);
        health = Mathf.Clamp(health + value, 0, maxHealth);
        if (showOnHealthBar)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }
    }
    /// <summary>
    /// 生命百分比恢复协程
    /// </summary>
    /// <param name="waitTime">挂起等待时间</param>
    /// <param name="percent"></param>
    /// <returns></returns>
    protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime,float percent)
    {
        while (health < maxHealth)
        {
            yield return waitTime;
            RestoreHealth(maxHealth * percent);
        }
    }

    /// <summary>
    /// 百分比减血协程
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health>0)
        {
            yield return waitTime;
            TakeDamage(maxHealth * percent);
        }
    }



}
