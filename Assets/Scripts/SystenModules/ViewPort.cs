using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 全游戏过程只会用到一个实例
/// 为了更方便访问，用单例
/// 创建泛型，并继承他
/// </summary>
public class ViewPort:Singleton<ViewPort>  //单例
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    //视窗下中心点
    float middleX;
    private void Start()
    {
        Camera mainCamera = Camera.main;
       
        //获取视窗左下角——右上角
        Vector2 bootomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));
        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x;
        minX = bootomLeft.x;
        minY = bootomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
        
    }

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition,float paddingX,float paddingY)
    {
        //限定移动范围
        Vector3 postion = Vector3.zero;
        postion.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
        postion.y = Mathf.Clamp(playerPosition.y, minY + paddingX, maxY - paddingY);
        return postion;
    }

    /// <summary>
    /// 敌人随机生成位置
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomEnemySpawnPosition(float paddingX,float paddingY)
    {
        Vector3 posotion = Vector3.zero;//初始化
        //赋值视窗右边坐标轴
        posotion.x = maxX + paddingX;
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//随机坐标
        return posotion;
    }

    /// <summary>
    /// 敌人在视窗内的随机移动位置(限制在右半部分)
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomRightHalfPosition(float paddingX,float paddingY)
    {
        Vector3 posotion = Vector3.zero;//初始化
        posotion.x = Random.Range(middleX, maxX - paddingX);//限制在右半部分
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//随机坐标
        return posotion;
    }
    /// <summary>
    /// 敌人在视窗内移动
    /// </summary>
    /// <param name="paddingX"></param>
    /// <param name="paddingY"></param>
    /// <returns></returns>
    public Vector3 RandomEnemyHalfPosition(float paddingX, float paddingY)
    {
        Vector3 posotion = Vector3.zero;//初始化
        posotion.x = Random.Range(middleX + paddingX, maxX - paddingX);
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//随机坐标
        return posotion;
    }
}
