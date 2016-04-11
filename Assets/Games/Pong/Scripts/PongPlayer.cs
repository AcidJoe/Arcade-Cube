using UnityEngine;
using System.Collections;

public class PongPlayer : MonoBehaviour
{
    public Rigidbody2D rb;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        float dir = Input.GetAxis("Vertical");

        rb.velocity = (new Vector2(0, dir)) * 500.0f; ;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            rb.velocity = (new Vector3(0, 0, 0) * Time.deltaTime);
        }
    }
}
