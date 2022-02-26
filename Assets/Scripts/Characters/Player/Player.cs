using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//直接添加刚体
public class Player : Character
{
    [SerializeField] StatsBar_HUD statsBar_HUD;

    [Header("生命值再生")]
    [SerializeField] bool regenerateHealth = true;
    [SerializeField] float healthRegenerateTime;//生命值再生时间
    [SerializeField,Range(0,1)] float healthRegeneratePercent;//生命值再生百分比


    [Header("-----输入-----")]
    [SerializeField] PlayerInput input;//输入

    [Header("-----移动-----")]
    [SerializeField] float moveSpeed = 6f;//速度

    //飞机再屏幕视窗里的限制值
    [SerializeField] float paddingX = 0.8f;
    [SerializeField] float paddingY = 0.3f;

    //加速减速时间
    [SerializeField] float accelerationTime = 0.35f;//加速时间
    [SerializeField] float decelerationTime = 0.75f;//减速时间

    //飞机旋转角度
    [SerializeField] float moveRotationAngle = 25f;//移动旋转角度

    [Header("-----开火-----")]
    [SerializeField] GameObject projectile1;//子弹预制体
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject projectile3;
    //子弹生成点(中-上-下)
    [SerializeField] Transform muzzleMiddle;
    [SerializeField] Transform muzzleTop;
    [SerializeField] Transform muzzleBottom;

    [SerializeField] AudioData projectileLaunchSFX;//开火音效
    //武器威力（取值0-2）
    [SerializeField,Range(0,2)]  int weaponPower = 0;
    //开火间隔
    [SerializeField] float fireInterval=1f;

    [Header("-----闪避-----")]
    [SerializeField] AudioData dodgeSFX;
    [SerializeField,Range(0,100)] int dodgeEnergyCost = 25;//闪避动消耗能量值
    [SerializeField] float maxRoll = 360f;//最大翻转角度
    [SerializeField] float rollSpeed = 360f;//翻转速度
    [SerializeField] Vector3 dodgeScale = new Vector3(0.5f, 0.5f, 0.5f);//存储玩家缩放值

    bool isDodgeing=false;

    float currentRoll;//翻转角度
    float dodgeDuration;//闪避时大小变化时间

    float t = 0f;
    Vector2 preiousVelocity;
    Quaternion previousRotation;
    
    WaitForSeconds waitForFireInterval;//协程等待私有变量
    WaitForSeconds waitHealthRegenerateTime;//生命值再生时间

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    //停用在参数协程
    Coroutine moveCoroutine;
    Coroutine healthRegenerateCoroutine;

    new Rigidbody2D rigidbody;//刚体
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
    /// 受伤函数
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        statsBar_HUD.UpdateStats(health, maxHealth);
        if (gameObject.activeSelf)//活跃
        {
            if (regenerateHealth)//血量
            {
                if (healthRegenerateCoroutine != null)//不为空
                {
                    StopCoroutine(healthRegenerateCoroutine);//停止协程
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
    void Move(Vector2 moveInput)//移动
    {
        if (moveCoroutine != null)//不移动时
        {
            //停止协程（完成之前停止）
            StopCoroutine(moveCoroutine);
        }


        //调用移动加速-----------------------------归一化                                                  
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime,
            moveInput.normalized * moveSpeed,
            //旋转角度-------------------------------------- - 不同方向旋转------------------------x轴
            Quaternion.AngleAxis(moveRotationAngle * moveInput.y, Vector3.right)
             ));
        //调用移动协程
        StartCoroutine(nameof(MovePositionLimitCoroutine));
    }

    void StopMove()//停止移动
    {
        if (moveCoroutine != null)//不移动时
        {
            //停止协程（完成之前停止）
            StopCoroutine(moveCoroutine);
        }
        //rigidbody.velocity = Vector2.zero;//归零
        //移动减速
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero, Quaternion.identity));
        //停止协程
        StopCoroutine(nameof(MovePositionLimitCoroutine));
    }


    /// <summary>
    /// 移动加速减速协程
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
            //线性插值函数
            rigidbody.velocity = Vector2.Lerp(preiousVelocity, moveVelocity, t);
            transform.rotation = Quaternion.Lerp(previousRotation, moveRotation, t);
            yield return waitForFixedUpdate;//挂起 
        }

    }
    /// <summary>
    /// 协程调用玩家移动位置
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
        //直接输入方法 会不执行
        //开始协程    查找名字
        StopCoroutine(nameof(FireCoroutine));
    }

    private void Fire()
    {
        //停止协程    查找名字
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
        //能量值消耗
        //玩家进行闪避时变成无敌
        //玩家染着x轴翻转
        isDodgeing = true;
        AudioManager.Instance.PlayRandomSFX(dodgeSFX);
        PlayerEnergy.Instance.Use(dodgeEnergyCost);
        collider.isTrigger = true;
        //玩家翻转
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
                 //第一种变化方式
                 //scale -= (Time.deltaTime / dodgeDuration) * Vector3.one;
                 //第二种变化方式
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
            //第三种变化方式
            transform.localScale = BezierCurve.QuadraticPoint(Vector3.one, Vector3.one, dodgeScale, currentRoll / maxRoll);
            yield return null;
        }
        collider.isTrigger = false;
        isDodgeing = false;
    }
    #endregion
}
