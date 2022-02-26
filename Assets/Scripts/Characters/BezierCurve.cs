using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 备赛曲线变化类
/// </summary>
public class BezierCurve
{
    /// <summary>
    /// 备赛曲线变化
    /// </summary>
    /// <param name="startPoint">开始点A</param>
    /// <param name="endPoint">结束点B</param>
    /// <param name="controlPoint">控制点C</param>
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
