using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Transform Parent { get; set; }

    [field: SerializeField]
    public GameObject Background { get; set; }
    [field: SerializeField]
    public GameObject Snake { get; set; }
    [field: SerializeField]
    public GameObject TailPiece { get; set; }
    [field: SerializeField]
    public GameObject Food { get; set; }
    [field: SerializeField]
    public GameObject FoodManager { get; set; }



    public GameBackground CreateBackground(Orientation orientation, int numberOfRowsAndColumns)
    {
        GameObject newGameObject = Instantiate(Background);
        newGameObject.transform.SetParent(Parent);

        if (newGameObject.TryGetComponent(out GameBackground background))
        {
            background.Construct(orientation);
        }
        else
        {
            print($"{nameof(Factory)} {gameObject.name}: background TryGetComponent(out GameBackground background) was false");
        }

        GridService gridService = newGameObject.GetComponent<GridService>();
        gridService.Construct(numberOfRowsAndColumns);

        return background;
    }

    public GameObject CreateDemonSprite(float scale)
    {
        GameObject newGameObject = new GameObject($"Caco {Guid.NewGuid()}");
        newGameObject.transform.SetParent(Parent);

        SpriteRenderer sr = newGameObject.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/demon");
        sr.sortingLayerName = "Enemy";
        newGameObject.transform.localScale = Vector2.one * scale;

        return newGameObject;
    }

    public Snake CreateSnake(GridService gridService, int numberOfRowsAndColumns)
    {
        GameObject newGameObject = Instantiate(Snake);
        newGameObject.transform.SetParent(Parent);
        newGameObject.transform.localScale = Vector2.one * gridService.unitScale;

        Snake snakeScript = newGameObject.GetComponent<Snake>();
        snakeScript.Construct(gridService, (int)(numberOfRowsAndColumns / 2f), (int)(numberOfRowsAndColumns / 2f), newGameObject.GetComponent<ArrowDirection>(), this);
        return snakeScript;
    }

    public Food CreateFood(int row, int column, GridService gridService, Snake snake, FoodManager foodManager)
    {
        GameObject newGameObject = Instantiate(Food);
        newGameObject.transform.SetParent(Parent);
        newGameObject.transform.localScale = Vector2.one * gridService.unitScale;
        newGameObject.transform.localPosition = gridService.GetPosition(column, row);

        Food foodScript = newGameObject.GetComponent<Food>();
        foodScript.Construct(row, column, snake, foodManager);
        return foodScript;
    }

    public FoodManager CreateFoodManager(GridService gridService, Snake snake)
    {
        GameObject newGameObject = Instantiate(FoodManager);
        newGameObject.transform.SetParent(Parent);
        newGameObject.transform.localScale = Vector2.one * gridService.unitScale;

        FoodManager foodManagerScript = newGameObject.GetComponent<FoodManager>();
        foodManagerScript.Construct(gridService, snake, this); // this = Factory, because we are inside the Factory class
        return foodManagerScript;
    }

    public TailPiece CreateTailPiece(int row, int column, float unitScale) 
    {
        GameObject newGameObject = Instantiate(TailPiece);
        newGameObject.transform.SetParent(Parent);
        newGameObject.transform.localScale = Vector2.one * unitScale;

        TailPiece tailPieceScript = newGameObject.GetComponent<TailPiece>();
        tailPieceScript.Construct(row, column);
        return tailPieceScript;

    }

}
