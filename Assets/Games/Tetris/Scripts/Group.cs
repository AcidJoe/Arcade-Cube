using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour
{
    TetrisManager tm;
    TetrisController tc;

    public AudioSource _as;
    public AudioSource drop;

    float lastFall = 0;

    float difficulty_mod;

    void Start()
    {
        switch (Game.current_difficulty)
        {
            case 1:
                difficulty_mod = 1.2f;
                break;
            case 2:
                difficulty_mod = 1.0f;
                break;
            case 3:
                difficulty_mod = 0.5f;
                break;
            case 4:
                difficulty_mod = 0.3f;
                break;
            case 5:
                difficulty_mod = 0.1f;
                break;
            case 6:
                difficulty_mod = 0.05f;
                break;
        }

        tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TetrisManager>();
        tc = GameObject.FindGameObjectWithTag("Manager").GetComponent<TetrisController>();
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            tm.GameOver();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _as.Play();

            transform.Rotate(0, 0, -90);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

         //Move Downwards and Fall
        else if (Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= 0.3f * difficulty_mod)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner_Tetris>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }

        else if (Time.time - lastFall >= 1 * difficulty_mod)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else {
                drop.Play();
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner_Tetris>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
