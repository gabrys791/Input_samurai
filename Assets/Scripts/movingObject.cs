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
    public float waitTime;
    private bool isWaiting;
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
            StartCoroutine(WaitAndSwitchTarget(pointB));
        }
        if(Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            StartCoroutine(WaitAndSwitchTarget(pointA));
        }
    }
    private IEnumerator WaitAndSwitchTarget(Transform newTarget)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        target = newTarget;
        isWaiting = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    // Usuniêcie gracza z hierarchii platformy
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
