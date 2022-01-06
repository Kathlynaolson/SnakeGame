using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<TailPiece> TailPieces { get; } = new List<TailPiece>();
    public Factory Factory { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }  
    public GridService GridService { get; set; }
    [field: SerializeField]
    public float TimeBetweenMoves { get; set; } = 0.5f;
    private IDirectionInput DirectionInput { get; set; }
    [field: SerializeField]
    public int Column { get; set; }
    [field: SerializeField]
    public int Row { get; set; }
    private int NumberOfRowsColumns { get; set; }
    public int NumberOfPieces { get; set; } = 1; // intialized to 1 to count for head piece
    
    private int _numberOfPiecesToGrow = 0;
    private float _timer = 0;
    private bool _play = false;


    public void Construct(GridService gridService, int column, int row, IDirectionInput directionInput, Factory factory)
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeColor(Color.green);

        this.GridService = gridService;
        this.Column = column;
        this.Row = row;
        this.NumberOfRowsColumns = gridService.numberOfRowsAndColumns;
        this.DirectionInput = directionInput;
        this.Factory = factory;

        transform.localPosition = GridService.GetPosition(Column, Row);
    }

    public void Grow()
    {
        _numberOfPiecesToGrow++;
    }

    public List<(int Row, int Column)> GetSnakeUnoccupiedPositions()
    {
        List<(int Row, int Column)> unoccupiedPositions = new List<(int Row, int Column)>();
        for (int i = 0; i < NumberOfRowsColumns; i++)
        {
            for (int j = 0; j < NumberOfRowsColumns; j++)
            {
                unoccupiedPositions.Add((i, j));
            }
        }

        unoccupiedPositions.Remove((this.Row, this.Column));

        foreach (var tailPiece in TailPieces)
        {
            unoccupiedPositions.Remove((tailPiece.Row, tailPiece.Column));
        }

        return unoccupiedPositions;
    }

    private void UpdatePosition(int oldHeadColumn, int oldHeadRow)
    {
        transform.localPosition = GridService.GetPosition(Column, Row);
        if (TailPieces.Count > 0)
        {
            for (int i = 0; i < TailPieces.Count; i++)
            {
                TailPieces[i].transform.localPosition = GridService.GetPosition(oldHeadColumn, oldHeadRow);

                int tempRow = TailPieces[i].Row;
                int tempColumn = TailPieces[i].Column;

                TailPieces[i].Column = oldHeadColumn;
                TailPieces[i].Row = oldHeadRow;

                oldHeadRow = tempRow;
                oldHeadColumn = tempColumn;
            }
        }
        if (_numberOfPiecesToGrow > 0)
        {
            _numberOfPiecesToGrow--;

            TailPiece newTailPiece = Factory.CreateTailPiece(oldHeadRow, oldHeadColumn, GridService.unitScale);

            TailPieces.Add(newTailPiece);
            newTailPiece.transform.localPosition = GridService.GetPosition(oldHeadColumn, oldHeadRow);
        }
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

                int oldHeadRow = Row;
                int oldHeadColumn = Column;
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
                    UpdatePosition(oldHeadColumn, oldHeadRow);
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
        Row = (int)(NumberOfRowsColumns / 2);
        Column = (int)(NumberOfRowsColumns / 2);
        transform.localPosition = GridService.GetPosition(Column, Row);

        _play = false;
    }

}
