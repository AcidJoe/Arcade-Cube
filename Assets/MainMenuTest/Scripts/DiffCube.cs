using UnityEngine;
using System.Collections;

public class DiffCube : MonoBehaviour
{
    public Rigidbody rb;

    public enum State { Unset, VeryEasy, Easy, Middlee, Hard, VeryHard, Hardcore }

    public State currentState;

    float checkTimer = 0.5f;

    public bool isStoped = false;

    public string ChosenDiff;
    public int difficultyN;

    void Awake()
    {
        ChosenDiff = "";

        currentState = State.Unset;
        rb = GetComponent<Rigidbody>();

        rb.AddTorque(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), ForceMode.Impulse);
    }

    void Update()
    {
        if (transform.position.y <= 1.1f && !isStoped)
        {
            checkTimer -= Time.deltaTime;
        }

        if (checkTimer <= 0 && !isStoped)
        {
            isStoped = true;
        }

        if (currentState == State.Unset && isStoped)
        {
            CheckGame();
        }
    }

    void SetState(State state)
    {
        switch (state)
        {
            case State.VeryEasy:
                ChosenDiff = "Легче некуда";
                break;
            case State.Easy:
                ChosenDiff = "Легко";
                break;
            case State.Middlee:
                ChosenDiff = "Средне";
                break;
            case State.Hard:
                ChosenDiff = "Сложно";
                break;
            case State.VeryHard:
                ChosenDiff = "Слишком сложно";
                break;
            case State.Hardcore:
                ChosenDiff = "Хардкор";
                break;
        }

        currentState = state;
    }

    void CheckGame()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.up, out hit);

        switch (hit.collider.gameObject.name)
        {
            case "VeryEasy":
                Game.current_difficulty = 0;
                SetState(State.VeryEasy);
                break;
            case "Easy":
                Game.current_difficulty = 1;
                SetState(State.Easy);
                break;
            case "Middle":
                Game.current_difficulty = 2;
                SetState(State.Middlee);
                break;
            case "Hard":
                Game.current_difficulty = 3;
                SetState(State.Hard);
                break;
            case "VeryHard":
                Game.current_difficulty = 4;
                SetState(State.VeryHard);
                break;
            case "Hardcore":
                Game.current_difficulty = 5;
                SetState(State.Hardcore);
                break;
        }
    }
}
