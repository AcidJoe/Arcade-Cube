using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    public GameObject Ball;
    private Transform CurrentTransform;
    private int speed = 150;

    public bool isBallSpawned = false;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentTransform = transform;
    }

    void Update()
    {
        if(!Ball && isBallSpawned)
        {
            Ball = GameObject.FindGameObjectWithTag("Ball");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentTransform = transform;
        if (Ball)
        {
            if (CurrentTransform.position.x < Ball.transform.position.x)
            {
                if ((int)CurrentTransform.position.y < (int)Ball.transform.position.y)
                {
                    rb.velocity = new Vector2(0, 1) * speed;
                }
                else if ((int)CurrentTransform.position.y > (int)Ball.transform.position.y)
                {
                    rb.velocity = new Vector2(0, -1) * speed;
                }
                else {
                    rb.velocity = new Vector2(0, 0) * speed;
                }
            }
        }
    }
}