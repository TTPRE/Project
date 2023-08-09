using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQQ : MonoBehaviour
{
    public Transform transform1;
    public Transform transform2;
    public Transform transform3;

    private Vector2 p1;
    private Vector2 p2;
    private Vector2 p3;

    public float ag;

    private void Start()
    {
        p1 = transform1.position;
        p2 = transform2.position;
        p3 = transform3.position;
    }
    private void Update()
    {
        //旋转点
        p2 = GetRotatePosition(p2,p1,ag);
        p3 = GetRotatePosition(p3,p1,ag);
        
        for (float i = 0f; i <= 1; i += 0.01f)
        {
            Vector3 point = Test.QuadraticBezier(p1, p2, p3, i);
            Debug.DrawLine(p1, point, Color.red);
        }


        for (float i = 0f; i <= 1; i += 0.01f)
        {
            Vector3 point = Test.QuadraticBezier(transform1.position, transform2.position, transform3.position, i);
            Debug.DrawLine(transform1.position, point, Color.red);
        }
    }

    private Vector2 GetRotatePosition(Vector2 targetPosition, Vector2 centerPosition, float angel)
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
