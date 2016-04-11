using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mesh : MonoBehaviour
{
    public Dictionary<GameObject, int> cells;

    public GameObject cell;
    int currId = 1;

    void Start()
    {
        cells = new Dictionary<GameObject, int>();

        for (float x = -15.0f; x <= 15.0f; x++)
        {
            for (float y = -10.0f; y <= 10.0f; y++)
            {
                cell = new GameObject();
                cell.transform.position = new Vector2(x, y);
                cells.Add(cell, currId);
                currId++;
            }
        }
    }

    void Update()
    {
        //List<GameObject> cls = new List<GameObject>(cells.Keys);

        //Debug.Log(cls.Count);

        //foreach(GameObject c in cls)
        //{
        //    Debug.DrawRay(c.transform.position, Vector3.forward, Color.blue);
        //}
    }
}
