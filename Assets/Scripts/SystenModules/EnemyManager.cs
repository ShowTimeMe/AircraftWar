using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ����������
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    public int WaveNumber => waveNumber;
    public float TimeBetweenWaves => timeBetweenWaves;

    [SerializeField] bool spawnEnmey = true;
    [SerializeField] GameObject waveUI;

    [SerializeField]GameObject[] enemyPrefabs;//��������
    [SerializeField] float timeBetweenSpawns = 3f;//����ʱ��

    [SerializeField] float timeBetweenWaves = 2f;

    
    [SerializeField]int minEnemyAmout = 4;
    [SerializeField]int maxEnemyAmout = 10;
    int waveNumber = 1;//���˲���
    int enemyAmout;//��������

    List<GameObject> enemyList;

    WaitForSeconds waitTimeBetweenSpawns;//���ɼ��ʱ�� 
    WaitForSeconds waitTimeBetweenWaves;//�ȴ�ÿ�����ʱ��
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
    /// ��������Э�̺���
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

    //�ѵ��˴�list��ɾ��
    public void RemoveFromList(GameObject enemy)=> enemyList.Remove(enemy);


}
