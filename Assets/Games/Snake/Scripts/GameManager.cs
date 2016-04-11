using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Mesh mesh;
    public Snake snake;

    public List<SnakeFood> food;

    public GameObject snakeFood;

    public LayerMask Uncreatable;

	void Start ()
    {
        food = new List<SnakeFood>();
	}

    void Update()
    {
        if (food.Count < 1)
            CreateFood();
    }

    void CreateFood()
    {
        List<GameObject> cells = new List<GameObject>(mesh.cells.Keys);
        List<GameObject> uncr = creatable();

        if (food.Count < 1)
        {
            int ran = Random.Range(1, 651);

            foreach (GameObject c in cells)
            {
                if (uncr.Contains(c))
                {
                   return;
                }

                if (mesh.cells[c] == ran)
                {
                    var newFood = Instantiate(snakeFood, c.transform.position, c.transform.rotation);
                    food.Add(newFood as SnakeFood);
                }
            }
        }
    }

    List<GameObject> creatable()
    {
        List<GameObject> cr = new List<GameObject>();

        List<GameObject> cells = new List<GameObject>(mesh.cells.Keys);

        foreach (GameObject c in cells)
        {
            if (Physics2D.OverlapCircle(c.transform.position,0.5f, Uncreatable))
            {
                cr.Add(c);
            }
        }
        return cr;
    }
}
