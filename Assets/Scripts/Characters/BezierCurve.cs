using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������߱仯��
/// </summary>
public class BezierCurve
{
    /// <summary>
    /// �������߱仯
    /// </summary>
    /// <param name="startPoint">��ʼ��A</param>
    /// <param name="endPoint">������B</param>
    /// <param name="controlPoint">���Ƶ�C</param>
    /// <param name="by"></param>
    /// <returns></returns>
    public static Vector3 QuadraticPoint(Vector3 startPoint, Vector3 endPoint, Vector3 controlPoint, float by)
    {
        return Vector3.Lerp(
            Vector3.Lerp(startPoint, controlPoint, by),
            Vector3.Lerp(controlPoint, endPoint, by),
            by);
    }
}
