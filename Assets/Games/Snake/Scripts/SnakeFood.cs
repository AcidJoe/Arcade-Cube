using UnityEngine;
using System.Collections;

public class SnakeFood : MonoBehaviour
{
    public SnakeManager sm;

    public AudioSource pickUp;

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Manager").GetComponent<SnakeManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pickUp.Play();
            Game.currentScore += 10;
            col.GetComponent<Snake>().lenght += 1;
        }

        sm.food.Clear();
        Destroy(gameObject);
    }
}
