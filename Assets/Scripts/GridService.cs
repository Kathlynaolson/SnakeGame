using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }

    Vector2 origin;
    public float unitScale;

    public int numberOfRowsAndColumns = 4;
    public Vector2 GetPosition( int column, int row )
    {
        return new Vector2( origin.x + ( column * unitScale ), origin.y - ( row * unitScale ) );
    }

    void SetOriginAndUnitScale()
    {
        Vector2 tlCorner = new Vector2(SpriteRenderer.bounds.min.x, SpriteRenderer.bounds.max.y);
        Vector2 trCorner = new Vector2(SpriteRenderer.bounds.max.x, SpriteRenderer.bounds.max.y);

        unitScale = Mathf.Abs( ( tlCorner.x - trCorner.x ) / numberOfRowsAndColumns );
        origin = new Vector2( ( tlCorner.x + ( unitScale/2 ) ), (tlCorner.y - ( unitScale/2 )) );
    }

    public void Construct(int numberOfRowsAndColumns)
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.numberOfRowsAndColumns = numberOfRowsAndColumns;
        SetOriginAndUnitScale();
    }
    
}
