using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int Row { get; set; }
    public int Column { get; set; }

    public void Construct(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }
}
