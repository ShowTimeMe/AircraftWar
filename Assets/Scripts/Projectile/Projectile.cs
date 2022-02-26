using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 子弹基类
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("伤害值")]
    [SerializeField] float damage;//伤害值
    [Header("碰撞特效")]
    [SerializeField]GameObject hitVFX;
    [Header("子弹命中音效")]
    [SerializeField] AudioData[] hitSFX;

    [SerializeField] float moveSpeed = 0;
    [SerializeField] protected Vector2 moveDirection;

    protected GameObject target;
    protected virtual void OnEnable()
    {
        StartCoroutine(MoveDirectly());
    }
    IEnumerator MoveDirectly()
    {
        while (gameObject.activeSelf)//不被销毁为真
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))//消耗性能少
        {
            character.TakeDamage(damage);
            //var contactPoint = collision.GetContact(0);//碰撞接触点
            //PoolManager.Release(hitVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            AudioManager.Instance.PlayRandomSFX(hitSFX);
            gameObject.SetActive(false);
        }
    }

}
