using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject per;
    public float doudu = 0f;
    public float angle = 0f;

    private Vector2 point;

    public float tLength = 1f;
    public float tWidth = 1f;
    public Vector2 direction = new Vector2(0,1);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        point = PointChange.GetRotatePosition(Vector2.right, Vector2.zero, angle);
        Debug.DrawLine(Vector2.zero, point, Color.red);


        direction = direction.normalized;
        Vector2 pointEnd = direction * tLength;
        Vector2 pointCenter = Vector2.Lerp(Vector2.zero,pointEnd,0.5f);
        Vector2 cDirection = PointChange.GetRotatePosition(direction,Vector2.zero,90).normalized;
        Vector2 cControl = cDirection * tWidth;
        Vector2 pointControl = cControl + pointCenter;

        Debug.DrawLine(pointCenter,pointCenter*2-pointControl,Color.green);
        Debug.DrawLine(Vector2.zero,pointEnd,Color.blue);
        Debug.DrawLine(pointCenter,pointControl,Color.blue);
        for (float i = 0f; i <= 1; i += 0.01f)
        {
            Vector3 point = Test.QuadraticBezier(Vector2.zero, pointControl, pointEnd, i);
            Debug.DrawLine(Vector2.zero, point, Color.red);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b1 = Instantiate(per, new Vector2(0, 20), Quaternion.identity);
            b1.GetComponent<Bullet1>().SetBezierPoint(new Vector2(0, 0), new Vector2(1, 0 + doudu), new Vector2(2, 0));
            GameObject b12 = Instantiate(per, new Vector2(0, 25), Quaternion.identity);
            b12.GetComponent<Bullet1>().SetBezierPoint(new Vector2(0, 0), new Vector2(1, 0 + doudu), new Vector2(2, 0));
            GameObject b2 = Instantiate(per, new Vector2(0, 15), Quaternion.identity);
            b2.GetComponent<Bullet1>().SetBezierPoint(new Vector2(0, 0), new Vector2(1, 0 - doudu), new Vector2(2, 0));
            GameObject b13 = Instantiate(per, new Vector2(0, 10), Quaternion.identity);
            b13.GetComponent<Bullet1>().SetBezierPoint(new Vector2(0, 0), new Vector2(1, 1 - doudu), new Vector2(2, 2));

            GameObject z = Instantiate(per, new Vector2(0, 5), Quaternion.identity);
            z.GetComponent<Bullet1>().SetBezierPoint(new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2));
        }
    }
}
