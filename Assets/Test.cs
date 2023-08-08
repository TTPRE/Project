using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        if (t > 1) t = 1;
        // 在二阶赛贝尔曲线上插值计算点的位置
        // B(t) = (1 - t)^2 * P0 + 2 * t * (1 - t) * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1 - t)^2 * P0
        point += 2 * u * t * p1; // 2 * t * (1 - t) * P1
        point += tt * p2; // t^2 * P2

        return point;
    }
}
