using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public SnakeManager sm;

    public AudioSource move;

    public GameObject snakeBit;
    public float spawnTime;
    public float defaultSpawn;

    Queue<GameObject> bits;

    public int lenght;

    float difficulty_mod;

    Vector3 moveFwd;
    Vector3 prevPos;
    Vector3 nextPos;
    Quaternion prevRot;

    public enum State { up, down, left, right }

    State currentState;

    bool isChoosen = false;

    void Start()
    {
        defaultSpawn = 0.5f;

        switch (Game.current_difficulty)
        {
            case 1:
                difficulty_mod = 0.9f;
                break;
            case 2:
                difficulty_mod = 1.0f;
                break;
            case 3:
                difficulty_mod = 2.0f;
                break;
            case 4:
                difficulty_mod = 3.0f;
                break;
            case 5:
                difficulty_mod = 4.2f;
                break;
            case 6:
                difficulty_mod = 6.0f;
                break;
        }

        bits = new Queue<GameObject>();
        setState(State.right);
        Invoke("MoveSnake", spawnTime);

        spawnTime = defaultSpawn / difficulty_mod;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "SnakeTail")
        {
            sm.GameOver();
            Destroy(gameObject);
        }
    }

    void MoveSnake()
    {
        move.Play();
        isChoosen = false;
        prevPos = transform.position;
        prevRot = transform.rotation;
        nextPos = transform.position += moveFwd;
        if (nextPos.x > 15.0f)
        {
            nextPos.x = -15.0f;
        }
        if (nextPos.x < -15.0f)
        {
            nextPos.x = 15.0f;
        }
        if (nextPos.y > 10.0f)
        {
            nextPos.y = -10.0f;
        }
        if (nextPos.y < -10.0f)
        {
            nextPos.y = 10.0f;
        }

        transform.position = nextPos;

        createBit();
        Invoke("MoveSnake", spawnTime);
    }

    void Update()
    {
        if (!isChoosen)
        {
            if (currentState == State.up || currentState == State.down)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    setState(State.right);
                    isChoosen = true;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    setState(State.left);
                    isChoosen = true;
                }
            }

            if (currentState == State.left || currentState == State.right)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    setState(State.up);
                    isChoosen = true;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    setState(State.down);
                    isChoosen = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnTime /= 3;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spawnTime *= 3;
        }
    }

    void createBit()
    {
        var newBit = Instantiate(snakeBit, prevPos, prevRot);
        GameObject bit = newBit as GameObject;
        bits.Enqueue(bit);

        if (bits.Count > lenght)
        {
            Destroy(bits.Dequeue());
        }
    }

    void setState(State state)
    {
        switch (state)
        {
            case State.up:
                moveFwd = new Vector3(0, 1.0f, 0);
                break;
            case State.down:
                moveFwd = new Vector3(0, -1.0f, 0);
                break;
            case State.left:
                moveFwd = new Vector3(-1.0f, 0, 0);
                break;
            case State.right:
                moveFwd = new Vector3(1.0f, 0, 0);
                break;
        }
        currentState = state;
    }
}
