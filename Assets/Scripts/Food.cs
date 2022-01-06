using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int Row { get; set; }
    public int Column { get; set; }
    private Snake Snake { get; set; }
    private FoodManager FoodManager { get; set; }

    public void Construct(int row, int column, Snake snake, FoodManager foodManager)
    {
        this.Row = row;
        this.Column = column;
        this.Snake = snake;
        this.FoodManager = foodManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnakeHead"))
        {
            print("Collided with FOOD");
            FoodManager.SpawnFood();
            Snake.Grow();
            Destroy(gameObject);
        }
    }
}
