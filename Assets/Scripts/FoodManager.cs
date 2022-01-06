﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GridService GridService { get; set; }
    public Snake Snake { get; set; }
    public Factory Factory { get; set; }
    public void Construct(GridService gridService, Snake snake, Factory factory)
    {
        this.GridService = gridService;
        this.Snake = snake;
        this.Factory = factory;
    }

    public void SpawnFood()
    {
        int row = 0;
        int column = 0;

        // Snake.NumberOfPieces
        // GridService.NumberOfRowsAndColumns
        int randomInt = Random.Range(0, GridService.numberOfRowsAndColumns - Snake.NumberOfPieces);


        Food food = Factory.CreateFood(row, column, GridService);
            
    }

}
