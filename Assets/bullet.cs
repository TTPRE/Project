using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Speed = 15f; // 子弹的速度
    private Vector2 direction = Vector2.right; // 子弹的移动方向
    private Rigidbody2D rb;

    public Vector2 pointBegin = new Vector2(0f, 0f);
    public Vector2 pointControl = new Vector2(1f, 1f);
    public Vector2 pointEnd = new Vector2(2f, 0f);

    public Transform transform1;
    public Transform transform2;
    public Transform transform3;
    private Vector3 lastpoint;

    private float timeCount = 0f;
    public float maxTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform1 = GameObject.Find("point1").transform;
        transform2 = GameObject.Find("point2").transform;
        transform3 = GameObject.Find("point3").transform;

        lastpoint = transform1.position;

        MoveBullet();
        rb.AddForce(Vector2.down,ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // timeCount += Time.deltaTime;
        // float t = timeCount / maxTime;
        // Vector3 point = Test.QuadraticBezier(transform1.position,transform2.position,transform3.position,t);
        // Vector2 newDirection = (Vector2)point - (Vector2)lastpoint;
        // lastpoint = point;
        // if(timeCount > maxTime)
        // {
        //     lastpoint = transform1.position;
        //     transform2.position = new Vector3(transform2.position.x,-transform2.position.y) ;
        //     timeCount = 0;
        // }
        // SetDirection(newDirection);
    }

    private void FixedUpdate()
    {
        timeCount += Time.deltaTime;
        float t = timeCount / maxTime;
        //Vector3 point = Test.QuadraticBezier(transform1.position, transform2.position, transform3.position, t);
        Vector3 point = Test.QuadraticBezier(pointBegin, pointControl, pointEnd, t);
        Vector2 newDirection = (Vector2)point - (Vector2)lastpoint;
        lastpoint = point;
        if (timeCount > maxTime)
        {
            //lastpoint = transform1.position;
            //transform2.position = new Vector3(transform2.position.x, -transform2.position.y);
            lastpoint = pointBegin;
            pointControl = new Vector2(pointControl.x,-pointControl.y);
            timeCount = 0;
        }
        SetDirection(newDirection);
    }
    private void MoveBullet()
    {
        rb.velocity = direction * Speed; // 使用固定的速度和方向来移动子弹
    }

    public void SetDirection(Vector2 BulletDirection)
    {
        direction = BulletDirection.normalized;
        MoveBullet();
    }
}
