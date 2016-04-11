using UnityEngine;
using System.Collections;

public class ArcadeCube : MonoBehaviour
{
    public Rigidbody rb;

    public enum State { Unset, Arkanoid, Asteroids, Snake, Pong, Tetris, Choise}

    public State currentState;

    float checkTimer = 0.5f;

    public bool isStoped = false;

    public string ChosenGame;
    public int GameScene;

    void Awake()
    {
        ChosenGame = "";

        currentState = State.Unset;
        rb = GetComponent<Rigidbody>();

        rb.AddTorque(Random.Range(0,180), Random.Range(0, 180), Random.Range(0, 180), ForceMode.Impulse);
    }

    void Update()
    {
        if(transform.position.y <= 1.1f && !isStoped)
        {
            checkTimer -= Time.deltaTime;
        }

        if(checkTimer <= 0 && !isStoped)
        {
            isStoped = true;
        }

        if(currentState == State.Unset && isStoped)
        {
            CheckGame();
        }
    }

    void SetState(State state)
    {
        switch (state)
        {
            case State.Arkanoid:
                GameScene = 2;
                ChosenGame = "Арканоид";
                break;
            case State.Asteroids:
                GameScene = 3;
                ChosenGame = "Астеройды";
                break;
            case State.Snake:
                GameScene = 6;
                ChosenGame = "Змейка";
                break;
            case State.Pong:
                GameScene = 4;
                ChosenGame = "Понг";
                break;
            case State.Tetris:
                GameScene = 5;
                ChosenGame = "Тетрис";
                break;
            case State.Choise:
                GameScene = 7;
                ChosenGame = "Выбор";
                break;
        }

        currentState = state;
    }

    void CheckGame()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.up, out hit);

        switch (hit.collider.gameObject.tag)
        {
            case "Ark":
                SetState(State.Arkanoid);
                break;
            case "Ast":
                SetState(State.Asteroids);
                break;
            case "Sn":
                SetState(State.Snake);
                break;
            case "Po":
                SetState(State.Pong);
                break;
            case "Tet":
                SetState(State.Tetris);
                break;
            case "Choi":
                SetState(State.Choise);
                break;
        }
    }
}
