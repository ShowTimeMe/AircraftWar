using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 敌人生成类
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    public int WaveNumber => waveNumber;
    public float TimeBetweenWaves => timeBetweenWaves;

    [SerializeField] bool spawnEnmey = true;
    [SerializeField] GameObject waveUI;

    [SerializeField]GameObject[] enemyPrefabs;//敌人数组
    [SerializeField] float timeBetweenSpawns = 3f;//生成时间

    [SerializeField] float timeBetweenWaves = 2f;

    
    [SerializeField]int minEnemyAmout = 4;
    [SerializeField]int maxEnemyAmout = 10;
    int waveNumber = 1;//敌人波数
    int enemyAmout;//敌人数量

    List<GameObject> enemyList;

    WaitForSeconds waitTimeBetweenSpawns;//生成间隔时间 
    WaitForSeconds waitTimeBetweenWaves;//等待每波间隔时间
    WaitUntil waitUntilNoEnemy;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<GameObject>();
        waitTimeBetweenSpawns = new WaitForSeconds(timeBetweenSpawns);
        waitTimeBetweenWaves = new WaitForSeconds(timeBetweenWaves);
        waitUntilNoEnemy = new WaitUntil(()=>enemyList.Count == 0);
    }

    IEnumerator Start()
    {
        while (spawnEnmey)
        {
            yield return waitUntilNoEnemy;
            waveUI.SetActive(true);
            yield return waitTimeBetweenWaves;
            waveUI.SetActive(false);
            yield return StartCoroutine(nameof(RandomLySopawnCoroutine));
        }
        
    }

    /// <summary>
    /// 敌人生成协程函数
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomLySopawnCoroutine()
    {
        enemyAmout = Mathf.Clamp(enemyAmout, minEnemyAmout + waveNumber / 3, maxEnemyAmout);
        for (int i = 0; i < enemyAmout; i++)
        {
            //var enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            //PoolManager.Release(enemy);
            enemyList.Add(PoolManager.Release(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]));

            yield return waitTimeBetweenSpawns;
        }

        waveNumber++;
    }

    //把敌人从list里删除
    public void RemoveFromList(GameObject enemy)=> enemyList.Remove(enemy);


}
