using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour, IDirectionInput
{ 
    private Direction Direction { get; set; }
    //private static KeyCode GetUp()
    //{
    //    return KeyCode.UpArrow;
    //}

    //private static KeyCode GetDown()
    //{
    //    return KeyCode.DownArrow;
    //}

    //private static KeyCode GetLeft()
    //{
    //    return KeyCode.LeftArrow;
    //}

    //private static KeyCode GetRight()
    //{
    //    return KeyCode.RightArrow;
    //}

    public KeyCode RightKey { get; } = KeyCode.RightArrow;
    public KeyCode LeftKey { get; } = KeyCode.LeftArrow;
    public KeyCode UpKey { get; } = KeyCode.UpArrow;
    public KeyCode DownKey { get; } = KeyCode.DownArrow;

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
