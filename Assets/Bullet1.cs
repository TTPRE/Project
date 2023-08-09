using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D rb;

    private Vector2 instantiatePosition;
    private Vector2 moveBegin;
    private Vector2 moveControl;
    private Vector2 moveEnd;
    private Vector2 pointBezierBegin = new Vector2(0, 0);
    private Vector2 pointBezierControl = new Vector2(1, 1);
    private Vector2 pointBezierEnd = new Vector2(2, 0);

    private float timeCount = 0f;
    public float maxTime = 1f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.right;
        instantiatePosition = transform.position;
        SetMovePointAtBezierPoint();
    }

    private void FixedUpdate()
    {
        StraightLineMove();
        //CirculateCurveMove();
        //OneCurveMove();
    }

    //设置方向
    public void SetDirection(Vector2 value)
    {
        if(value == Vector2.zero) return;
        direction = value.normalized;
    }

    //设置贝塞尔参考点
    public void SetBezierPoint(Vector2 pB, Vector2 pC, Vector2 pE)
    {
        Debug.Log(pB + " " + pC + " " + pE);
        pointBezierBegin = pB;
        pointBezierControl = pC;
        pointBezierEnd = pE;
        SetMovePointAtBezierPoint();
    }

    //设置真正的贝塞尔移动路径
    public void SetMovePointAtBezierPoint()
    {
        moveBegin = instantiatePosition;
        moveControl = (pointBezierControl - pointBezierBegin) + instantiatePosition;
        moveEnd = (pointBezierEnd - pointBezierBegin) + instantiatePosition;
    }

    //获取贝塞尔进度t的点
    public Vector2 GetNextPosition(float t)
    {
        Vector2 nextBezierPoint = PointChange.QuadraticBezier(moveBegin, moveControl, moveEnd, t);
        return nextBezierPoint;
    }
    
    //循环曲线运动
    public void CirculateCurveMove()
    {
        timeCount += Time.deltaTime;
        Vector2 nextPosition = GetNextPosition(timeCount / maxTime);
        rb.MovePosition(nextPosition);
        if (timeCount > maxTime)
        {
            instantiatePosition = transform.position;
            pointBezierControl = new Vector2(pointBezierControl.x, pointBezierBegin.y * 2 - pointBezierControl.y);
            SetMovePointAtBezierPoint();
            timeCount = 0f;
        }
    }

    //一次曲线运动 后续直线运动
    public void OneCurveMove()
    {
        timeCount += Time.deltaTime;
        Vector2 nextPosition = GetNextPosition(timeCount / maxTime);
        rb.MovePosition(nextPosition);
        //设置方向
        SetDirection(nextPosition - (Vector2)transform.position);
        if (timeCount > maxTime)
        {
            //初始移动位置改变
            instantiatePosition = transform.position;
            //计算贝塞尔控制点并调用Set函数
            float bezierCurveLength = PointChange.GetBezierLength(moveBegin, moveControl, moveEnd);
            Vector2 tempEnd = direction * bezierCurveLength;
            Vector2 tempControl = Vector2.Lerp(pointBezierBegin, tempEnd, 0.5f);
            SetBezierPoint(pointBezierBegin, tempControl, tempEnd);
            SetMovePointAtBezierPoint();
            timeCount = 0f;
        }
    }

    public void StraightLineMove()
    {
        timeCount += Time.deltaTime;
        Vector2 nextPosition = GetNextPosition(timeCount / maxTime);
        rb.MovePosition(nextPosition);
        if (timeCount > maxTime)
        {
            instantiatePosition = transform.position;
            SetMovePointAtBezierPoint();
            timeCount = 0f;
        }
    }
}
