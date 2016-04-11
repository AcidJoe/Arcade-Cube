using UnityEngine;
using System.Collections;

public class AsteroidMid : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 dir;

    float speed;

    public Ship playerShip;

    public AudioSource bangSound;

    float difficulty_mod;

    public GameObject astersm1;
    public GameObject astersm2;
    public GameObject astersm3;
    public GameObject astersm4;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        dir = new Vector2(transform.position.x, transform.position.y) - new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));

        dir = dir.normalized;

        bangSound = GetComponent<AudioSource>();

        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

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

        speed = Random.Range(1.0f, 1.3f);

        rb.velocity = dir * speed * difficulty_mod;
    }

    void Update()
    {

    }

    GameObject ranAster()
    {
        int i = Random.Range(1, 4);

        switch (i)
        {
            case 1:
                return astersm1;
            case 2:
                return astersm2;
            case 3:
                return astersm3;
            case 4:
                return astersm4;
        }

        return astersm1;
    }

    void Crack()
    {
        Instantiate(ranAster(), transform.position, Quaternion.identity);
        Instantiate(ranAster(), transform.position, Quaternion.identity);
        Instantiate(ranAster(), transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            playerShip.bang.Play();
            Game.currentScore += 30;
            Destroy(col.gameObject);
            Crack();
        }

        if (col.gameObject.tag == "Player")
        {
            bangSound.enabled = true;
            bangSound.Play();
            col.SendMessage("Die");
        }
    }
}
