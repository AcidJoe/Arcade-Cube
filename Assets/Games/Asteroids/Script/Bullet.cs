using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public Ship ship; 

    float lifetime;

	void Awake()
    {
        lifetime = 2.0f;
        rb = GetComponent<Rigidbody2D>();
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        rb.AddForce(ship.transform.up * 600.0f);
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
