using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//ֱ����Ӹ���
public class Player : Character
{
    [SerializeField] StatsBar_HUD statsBar_HUD;

    [Header("����ֵ����")]
    [SerializeField] bool regenerateHealth = true;
    [SerializeField] float healthRegenerateTime;//����ֵ����ʱ��
    [SerializeField,Range(0,1)] float healthRegeneratePercent;//����ֵ�����ٷֱ�


    [Header("-----����-----")]
    [SerializeField] PlayerInput input;//����

    [Header("-----�ƶ�-----")]
    [SerializeField] float moveSpeed = 6f;//�ٶ�

    //�ɻ�����Ļ�Ӵ��������ֵ
    [SerializeField] float paddingX = 0.8f;
    [SerializeField] float paddingY = 0.3f;

    //���ټ���ʱ��
    [SerializeField] float accelerationTime = 0.35f;//����ʱ��
    [SerializeField] float decelerationTime = 0.75f;//����ʱ��

    //�ɻ���ת�Ƕ�
    [SerializeField] float moveRotationAngle = 25f;//�ƶ���ת�Ƕ�

    [Header("-----����-----")]
    [SerializeField] GameObject projectile1;//�ӵ�Ԥ����
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject projectile3;
    //�ӵ����ɵ�(��-��-��)
    [SerializeField] Transform muzzleMiddle;
    [SerializeField] Transform muzzleTop;
    [SerializeField] Transform muzzleBottom;

    [SerializeField] AudioData projectileLaunchSFX;//������Ч
    //����������ȡֵ0-2��
    [SerializeField,Range(0,2)]  int weaponPower = 0;
    //������
    [SerializeField] float fireInterval=1f;

    [Header("-----����-----")]
    [SerializeField] AudioData dodgeSFX;
    [SerializeField,Range(0,100)] int dodgeEnergyCost = 25;//���ܶ���������ֵ
    [SerializeField] float maxRoll = 360f;//���ת�Ƕ�
    [SerializeField] float rollSpeed = 360f;//��ת�ٶ�
    [SerializeField] Vector3 dodgeScale = new Vector3(0.5f, 0.5f, 0.5f);//�洢�������ֵ

    bool isDodgeing=false;

    float currentRoll;//��ת�Ƕ�
    float dodgeDuration;//����ʱ��С�仯ʱ��

    float t = 0f;
    Vector2 preiousVelocity;
    Quaternion previousRotation;
    
    WaitForSeconds waitForFireInterval;//Э�̵ȴ�˽�б���
    WaitForSeconds waitHealthRegenerateTime;//����ֵ����ʱ��

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    //ͣ���ڲ���Э��
    Coroutine moveCoroutine;
    Coroutine healthRegenerateCoroutine;

    new Rigidbody2D rigidbody;//����
    new Collider2D collider;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        dodgeDuration = maxRoll / rollSpeed;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onFire += Fire;
        input.onStopFire += StopFire;
        input.onDodge += Dodge;
    }

    

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;

        input.onFire -= Fire;
        input.onStopFire -= StopFire;
        input.onDodge -= Dodge;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = 0f;
        waitForFireInterval = new WaitForSeconds(fireInterval);
        waitHealthRegenerateTime = new WaitForSeconds(healthRegenerateTime);
        input.EnableGamePlayerInput();

        statsBar_HUD.InitIalize(health, maxHealth);

    }


    /// <summary>
    /// ���˺���
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        statsBar_HUD.UpdateStats(health, maxHealth);
        if (gameObject.activeSelf)//��Ծ
        {
            if (regenerateHealth)//Ѫ��
            {
                if (healthRegenerateCoroutine != null)//��Ϊ��
                {
                    StopCoroutine(healthRegenerateCoroutine);//ֹͣЭ��
                }
                StartCoroutine(HealthRegenerateCoroutine(waitHealthRegenerateTime, healthRegeneratePercent));
            }
        }
    }

    public override void RestoreHealth(float value)
    {
        base.RestoreHealth(value);
        statsBar_HUD.UpdateStats(health, maxHealth);
    }
    public override void Die()
    {
        statsBar_HUD.UpdateStats(0, maxHealth);
        base.Die();
    }


    #region MOVE
    void Move(Vector2 moveInput)//�ƶ�
    {
        if (moveCoroutine != null)//���ƶ�ʱ
        {
            //ֹͣЭ�̣����֮ǰֹͣ��
            StopCoroutine(moveCoroutine);
        }


        //�����ƶ�����-----------------------------��һ��                                                  
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime,
            moveInput.normalized * moveSpeed,
            //��ת�Ƕ�-------------------------------------- - ��ͬ������ת------------------------x��
            Quaternion.AngleAxis(moveRotationAngle * moveInput.y, Vector3.right)
             ));
        //�����ƶ�Э��
        StartCoroutine(nameof(MovePositionLimitCoroutine));
    }

    void StopMove()//ֹͣ�ƶ�
    {
        if (moveCoroutine != null)//���ƶ�ʱ
        {
            //ֹͣЭ�̣����֮ǰֹͣ��
            StopCoroutine(moveCoroutine);
        }
        //rigidbody.velocity = Vector2.zero;//����
        //�ƶ�����
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero, Quaternion.identity));
        //ֹͣЭ��
        StopCoroutine(nameof(MovePositionLimitCoroutine));
    }


    /// <summary>
    /// �ƶ����ټ���Э��
    /// </summary>
    /// <param name="moveVelocity"></param>
    /// <returns></returns>
    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity, Quaternion moveRotation)
    {
        t = 0f;
        preiousVelocity = rigidbody.velocity;
        previousRotation = transform.rotation;
        while (t < 1f)
        {
            t += Time.fixedDeltaTime / time;
            //���Բ�ֵ����
            rigidbody.velocity = Vector2.Lerp(preiousVelocity, moveVelocity, t);
            transform.rotation = Quaternion.Lerp(previousRotation, moveRotation, t);
            yield return waitForFixedUpdate;//���� 
        }

    }
    /// <summary>
    /// Э�̵�������ƶ�λ��
    /// </summary>
    /// <returns></returns>
    IEnumerator MovePositionLimitCoroutine()
    {
        while (true)
        {
            transform.position = ViewPort.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);
            yield return null;
        }
    }

    #endregion

    #region FIRE

    private void StopFire()
    {
        //ֱ�����뷽�� �᲻ִ��
        //��ʼЭ��    ��������
        StopCoroutine(nameof(FireCoroutine));
    }

    private void Fire()
    {
        //ֹͣЭ��    ��������
        StartCoroutine(nameof(FireCoroutine));
    }


    IEnumerator FireCoroutine()
    {
        
        while (true)
        {
            switch (weaponPower)
            {
                case 0:
                    PoolManager.Release(projectile1, muzzleMiddle.position);
                    break;
                case 1:
                    PoolManager.Release(projectile1, muzzleTop.position);
                    PoolManager.Release(projectile1, muzzleBottom.position);
                    break;
                case 2:
                    PoolManager.Release(projectile1, muzzleMiddle.position);
                    PoolManager.Release(projectile2, muzzleTop.position);
                    PoolManager.Release(projectile3, muzzleBottom.position);
                    break;
                default:
                    break;
            }
            AudioManager.Instance.PlayRandomSFX(projectileLaunchSFX);
            yield return waitForFireInterval;
        }
    }


    #endregion


    #region DODGE
    void Dodge()
    {
        Console.WriteLine("11111111111111111");
        if (isDodgeing || !PlayerEnergy.Instance.IsEnough(dodgeEnergyCost)) return;

        StartCoroutine(nameof(DodgeCoroutine));
    }

    IEnumerator DodgeCoroutine()
    {
        //����ֵ����
        //��ҽ�������ʱ����޵�
        //���Ⱦ��x�ᷭת
        isDodgeing = true;
        AudioManager.Instance.PlayRandomSFX(dodgeSFX);
        PlayerEnergy.Instance.Use(dodgeEnergyCost);
        collider.isTrigger = true;
        //��ҷ�ת
        currentRoll = 0f;

        //var scale = transform.localScale;
        //var t1 = 0f;
        //var t2 = 0f;

        while (currentRoll < maxRoll)
        {
            currentRoll += rollSpeed * Time.deltaTime;
            transform.rotation=Quaternion.AngleAxis(currentRoll, Vector3.right);
            #region scale
            /* if (currentRoll < maxRoll / 2f)
             {
                 //��һ�ֱ仯��ʽ
                 //scale -= (Time.deltaTime / dodgeDuration) * Vector3.one;
                 //�ڶ��ֱ仯��ʽ
                 //t1 += Time.deltaTime / dodgeDuration;
                 //transform.localScale = Vector3.Lerp(transform.localScale, dodgeScale, t1);
             }
             else
             {
                 //scale += (Time.deltaTime / dodgeDuration) * Vector3.one;
                 //t2 += Time.deltaTime / dodgeDuration;
                 //transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, t2);
             }*/
            //transform.localScale = scale;
            #endregion
            //�����ֱ仯��ʽ
            transform.localScale = BezierCurve.QuadraticPoint(Vector3.one, Vector3.one, dodgeScale, currentRoll / maxRoll);
            yield return null;
        }
        collider.isTrigger = false;
        isDodgeing = false;
    }
    #endregion
}
