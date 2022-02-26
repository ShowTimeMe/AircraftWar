using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ɫ��
/// </summary>
public class Character : MonoBehaviour
{
    [Header("��ɫ������Ч")]
    [SerializeField] GameObject deathVFX;
    [Header("��ɫ������ը��Ч")]
    [SerializeField] AudioData deathSFX;
    [Header("��ɫ�������ֵ")]
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
    /// Ѫ����������ʾ
    /// </summary>
    public void ShowOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.InitIalize(health, maxHealth);
    }
    /// <summary>
    /// Ѫ������������ʾ
    /// </summary>
    public void HideOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// ����ɫ����ʱ
    /// �ɱ�������д
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
    /// ��ɫ����
    /// </summary>
    public virtual void Die()
    {
        health = 0f;
        AudioManager.Instance.PlayRandomSFX(deathSFX);
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ɫ�ָ�����ֵ
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
    /// �����ٷֱȻָ�Э��
    /// </summary>
    /// <param name="waitTime">����ȴ�ʱ��</param>
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
    /// �ٷֱȼ�ѪЭ��
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
