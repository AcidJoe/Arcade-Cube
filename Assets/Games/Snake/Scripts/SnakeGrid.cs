using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeGrid : MonoBehaviour
{
    [SerializeField]
    public List<SnakeCell> cells;
    public SnakeCell cell;

    public bool isReady = false;

    void Start()
    {
        cells = new List<SnakeCell>();

        for (int x = -15; x <= 15; x++)
        {
            for (int y = -10; y <= 10; y++)
            {
                cell = new SnakeCell(x, y);
                cells.Add(cell);
            }
        }

        Debug.Log(cells.Count);
        isReady = true;
    }
	
	void Update ()
    {
	
	}
}

public class SnakeCell
{
    public int x;
    public int y;

    Vector2 pos;

    public SnakeCell(int X, int Y)
    {
        x = X;
        y = Y;

        pos = new Vector2(x, y);
    }
}
