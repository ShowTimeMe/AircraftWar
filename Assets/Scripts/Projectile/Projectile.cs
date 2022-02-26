using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ӵ�����
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("�˺�ֵ")]
    [SerializeField] float damage;//�˺�ֵ
    [Header("��ײ��Ч")]
    [SerializeField]GameObject hitVFX;
    [Header("�ӵ�������Ч")]
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
        while (gameObject.activeSelf)//��������Ϊ��
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))//����������
        {
            character.TakeDamage(damage);
            //var contactPoint = collision.GetContact(0);//��ײ�Ӵ���
            //PoolManager.Release(hitVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            AudioManager.Instance.PlayRandomSFX(hitSFX);
            gameObject.SetActive(false);
        }
    }

}
