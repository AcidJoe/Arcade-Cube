using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour
{
    public float speed = 100.0f;

    public Rigidbody2D rb;

    public AudioSource pong;

    float difficulty_mod;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();

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
    }
	
	void Update ()
    {
	
	}

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        float f = (ballPos.y - racketPos.y) / racketWidth; ;
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.tag == "Player")
        {
            pong.Play();
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.y);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed * difficulty_mod;
        }

        if (col.gameObject.tag == "AI")
        {
            pong.Play();
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.y);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed * difficulty_mod;
        }

        if (col.gameObject.tag == "PlayerGoal")
        {
            col.gameObject.GetComponentInParent<PongManager>().playerGoal();
        }

        if (col.gameObject.tag == "AIGoal")
        {
            col.gameObject.GetComponentInParent<PongManager>().AIGoal();
        }
    }
}
