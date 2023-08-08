using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChange : MonoBehaviour
{
    //贝塞尔曲线 p0:起始点 p1:控制点 p2:结束点 t:比率进度[0,1]
    public static Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        // if (t > 1) t = 1;
        // // 在二阶赛贝尔曲线上插值计算点的位置
        // // B(t) = (1 - t)^2 * P0 + 2 * t * (1 - t) * P1 + t^2 * P2
        // float u = 1 - t;
        // float tt = t * t;
        // float uu = u * u;

        // Vector2 point = uu * p0; // (1 - t)^2 * P0
        // point += 2 * u * t * p1; // 2 * t * (1 - t) * P1
        // point += tt * p2; // t^2 * P2

        Vector2 p0p1 = Vector2.Lerp(p0,p1,t);
        Vector2 p1p2 = Vector2.Lerp(p1,p2,t);

        return Vector2.Lerp(p0p1,p1p2,t);
    }

    //获取贝塞尔曲线长度 通过计算贝赛尔曲线上多段线段的距离总和求近似曲线长度
    public static float GetBezierLength(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float length = 0f;
        Vector2 pointFlag = p0;
        float pointNum = 60f; //分割曲线上 pointNum+1 个点(包括起始点)
        for (float pointCount = 1f; pointCount <= pointNum; ++pointCount)
        {
            Vector2 lastBezierPoint = QuadraticBezier(p0, p1, p2, pointCount / pointNum);
            length += Vector2.Distance(pointFlag,lastBezierPoint);
            pointFlag = lastBezierPoint;
        }
        return length;
    }

    //获取一个点绕另一个点旋转一定角度后的点 targetPosition:目标点 centerPosition:旋转的中心点 angel:旋转角度
    public static Vector2 GetRotatePosition(Vector2 targetPosition, Vector2 centerPosition, float angel)
    {
        //X = (Ax - Bx) * cos(angle) - (Ay - By) * sin(angle) + Bx
        //Y = (Ax - Bx) * sin(angle) - (Ay - by) * cos(angle) + By
        float endX = (targetPosition.x - centerPosition.x) * Mathf.Cos(angel * Mathf.Deg2Rad) -
                    (targetPosition.y - centerPosition.y) * Mathf.Sin(angel * Mathf.Deg2Rad) + centerPosition.x;
        float endY = (targetPosition.x - centerPosition.x) * Mathf.Sin(angel * Mathf.Deg2Rad) +
                    (targetPosition.y - centerPosition.y) * Mathf.Cos(angel * Mathf.Deg2Rad) + centerPosition.y;
        return new Vector2(endX, endY);
    }
}
