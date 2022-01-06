using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackground : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public GridService Grid { get; set; }

    public void Construct(Orientation orientation)
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            SpriteRenderer = spriteRenderer;
        }
        else
        {
            SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/square");
        }

        //print($"Orientation passed is {orientation}");

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        switch (orientation)
        {
            case Orientation.Portrait:
                transform.localScale = new Vector2(width, width);
                break;
            case Orientation.Landscape:
                transform.localScale = new Vector2(height, height);
                break;
        }
    }




}
