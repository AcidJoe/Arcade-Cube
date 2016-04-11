using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isActivate;

    public float speed;

    public GameObject player;

    public AudioSource ping;
    public AudioSource bang;

    public Vector3 pos;

    public float multiplier = 1;
    float difficulty_mod;


    void Start ()
    {
        multiplier = 1;

        switch (Game.current_difficulty)
        {
            case 1:
                difficulty_mod = 0.9f;
                break;
            case 2:
                difficulty_mod = 1.0f;
                break;
            case 3:
                difficulty_mod = 1.5f;
                break;
            case 4:
                difficulty_mod = 2.0f;
                break;
            case 5:
                difficulty_mod = 2.2f;
                break;
            case 6:
                difficulty_mod = 3.0f;
                break;
        }

        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;

        multiplier *= difficulty_mod;

        isActivate = false;
    }
	
	void Update ()
    {
        if (!isActivate && player != null)
        {
            pos.x = player.transform.position.x;
            pos.y = player.transform.position.y + 13.0f;

            transform.position = pos;
        }

        if (Input.GetButtonDown("Jump") && !isActivate)
        {
            isActivate = true;
            rb.velocity = Vector2.up * speed * multiplier;
        }
	}

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        float f = (ballPos.x - racketPos.x) / racketWidth; ;
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ping.Play();

        // Hit the Racket?
        if (col.gameObject.tag == "Player")
        {
            // Calculate hit Factor
            float x = hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed * multiplier;
        }
    }
}
