using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [field: SerializeField]
    public Factory Factory { get; set; }
    [field: SerializeField]
    public Orientation Orientation { get; set; } = Orientation.Landscape;

    private void Awake()
    {
        Factory = Instantiate(Factory);
        Factory.transform.SetParent(transform);
        Factory.Parent = transform;

        int numberOfRowsAndColumns = 20;
        GameBackground gameBackground = Factory.CreateBackground(Orientation, numberOfRowsAndColumns);
        gameBackground.transform.localPosition = Vector3.zero;

        GridService gridService = gameBackground.GetComponent<GridService>();
        
        Snake snake = Factory.CreateSnake(gridService, numberOfRowsAndColumns);

        FoodManager foodManager = Factory.CreateFoodManager(gridService, snake);

        foodManager.SpawnFood();
        


    }

}
