using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObject : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Rigidbody2D rb;
    public float speed;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, pointA.position) < 0.1f)
        {
            target = pointB;
        }
        if(Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            target = pointA;
        }
    }
}
