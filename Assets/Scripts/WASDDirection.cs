using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDDirection : MonoBehaviour, IDirectionInput
{
    private Direction Direction { get; set; }

    private KeyCode RightKey { get; } = KeyCode.D;
    private KeyCode LeftKey { get; } = KeyCode.A;
    private KeyCode DownKey { get; } = KeyCode.S;
    private KeyCode UpKey { get; } = KeyCode.W;

    public Direction GetDirection()
    {
        return Direction;
    }

    void Update()
    {

        if (Input.GetKeyDown(RightKey))
        {
            Direction = Direction.Right;
        }
        if (Input.GetKeyDown(LeftKey))
        {
            Direction = Direction.Left;
        }
        if (Input.GetKeyDown(DownKey))
        {
            Direction = Direction.Down;
        }
        if (Input.GetKeyDown(UpKey))
        {
            Direction = Direction.Up;
        }

    }

}
