using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<TailPiece> TailPieces { get; } = new List<TailPiece>();
    public SpriteRenderer SpriteRenderer { get; set; }  
    public GridService GridService { get; set; }
    [field: SerializeField]
    public float TimeBetweenMoves { get; set; } = 0.5f;
    private IDirectionInput DirectionInput { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }
    private int NumberOfRowsColumns { get; set; }
    public int NumberOfPieces { get; set; } = 1; // intialized to 1 to count for head piece

    private float _timer = 0;
    private bool _play = false;


    public void Construct(GridService gridService, int column, int row, IDirectionInput directionInput)
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeColor(Color.green);

        this.GridService = gridService;
        this.Column = column;
        this.Row = row;
        this.NumberOfRowsColumns = gridService.numberOfRowsAndColumns;
        this.DirectionInput = directionInput;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.localPosition = GridService.GetPosition(Column, Row);
    }

    public void ChangeColor(Color color)
    {
        SpriteRenderer.color = color;
    }

    private void Update()
    {
        if (_play)
        {
            _timer += Time.deltaTime;

            if (_timer > TimeBetweenMoves)
            {
                _timer = 0; // Reset timer

                switch (DirectionInput.GetDirection())
                {
                    case Direction.Up:
                        Row--;
                        break;
                    case Direction.Down:
                        Row++;
                        break;
                    case Direction.Left:
                        Column--;
                        break;
                    case Direction.Right:
                        Column++;
                        break;
                    case Direction.Null:
                        break;
                }

                if (IsRowAndColumnOutOfBounds())
                {
                    print("Oh no! You are out of bounds");
                    ResetSnake();
                }
                else
                {
                    UpdatePosition();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _play = !_play;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSnake();
        }
    }

    private bool IsRowAndColumnOutOfBounds()
    {
        return Row < 0 || Column < 0 || Row >= NumberOfRowsColumns || Column >= NumberOfRowsColumns;
    }

    private void ResetSnake()
    {
        Row = NumberOfRowsColumns / 2;
        Column = NumberOfRowsColumns / 2;
        transform.localPosition = GridService.GetPosition(Column, Row);

        _play = false;
    }

}
