using UnityEngine;
using System.Collections;

public class TetrisController : MonoBehaviour
{
    public int direction = 0;
    public bool isRot = false;
    public bool isDown = false;

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    public void SetRotn(bool rot)
    {
        isRot = rot;
    }

    public void SetDown(bool down)
    {
        isDown = down;
    }
}
