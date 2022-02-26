using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ȫ��Ϸ����ֻ���õ�һ��ʵ��
/// Ϊ�˸�������ʣ��õ���
/// �������ͣ����̳���
/// </summary>
public class ViewPort:Singleton<ViewPort>  //����
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    //�Ӵ������ĵ�
    float middleX;
    private void Start()
    {
        Camera mainCamera = Camera.main;
       
        //��ȡ�Ӵ����½ǡ������Ͻ�
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
        //�޶��ƶ���Χ
        Vector3 postion = Vector3.zero;
        postion.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
        postion.y = Mathf.Clamp(playerPosition.y, minY + paddingX, maxY - paddingY);
        return postion;
    }

    /// <summary>
    /// �����������λ��
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomEnemySpawnPosition(float paddingX,float paddingY)
    {
        Vector3 posotion = Vector3.zero;//��ʼ��
        //��ֵ�Ӵ��ұ�������
        posotion.x = maxX + paddingX;
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//�������
        return posotion;
    }

    /// <summary>
    /// �������Ӵ��ڵ�����ƶ�λ��(�������Ұ벿��)
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomRightHalfPosition(float paddingX,float paddingY)
    {
        Vector3 posotion = Vector3.zero;//��ʼ��
        posotion.x = Random.Range(middleX, maxX - paddingX);//�������Ұ벿��
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//�������
        return posotion;
    }
    /// <summary>
    /// �������Ӵ����ƶ�
    /// </summary>
    /// <param name="paddingX"></param>
    /// <param name="paddingY"></param>
    /// <returns></returns>
    public Vector3 RandomEnemyHalfPosition(float paddingX, float paddingY)
    {
        Vector3 posotion = Vector3.zero;//��ʼ��
        posotion.x = Random.Range(middleX + paddingX, maxX - paddingX);
        posotion.y = Random.Range(minY + paddingY, maxY - paddingY);//�������
        return posotion;
    }
}
