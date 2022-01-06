using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemy : MonoBehaviour
{
    public float _speed = 2;
    public float _sizeOfMovement = 3;
    public int _modulusBy = 5;
    private int _counter = 5 + 1;

    public Rigidbody2D Rigidbody { get; set; }

    public Vector3 StartPosition { get; set; }

    public EnemyMovementType EnemyMovementType { get; set; } = EnemyMovementType.Circle;


    public void Construct()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            EnemyMovementType = EnemyMovementType.Circle;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EnemyMovementType = EnemyMovementType.Lemniscate;
        }
    }

    private void FixedUpdate()
    {
        float t = Time.time * _speed; //Random.value * 2 * Mathf.PI;
        if (_counter++ % _modulusBy == 0)
        {
            _counter = _modulusBy + 1;
            switch (EnemyMovementType)
            {
                case EnemyMovementType.Circle:
                    Rigidbody.MovePosition(StartPosition + GetPointFromCircle(t));
                    break;
                case EnemyMovementType.Lemniscate:
                    Rigidbody.MovePosition(StartPosition + GetPointFromLemniscate(t));
                    break;
            } 
        }
    }

    private Vector3 GetPointFromLemniscate(float t)
    {
        float onePlusSinSquared = 1 + Mathf.Pow(Mathf.Sin(t), 2);
        return new Vector3(_sizeOfMovement * Mathf.Cos(t) / onePlusSinSquared, _sizeOfMovement * Mathf.Sin(t) * Mathf.Cos(t) / onePlusSinSquared, 0);
    }

    private Vector3 GetPointFromCircle(float t)
    {
        return new Vector3(_sizeOfMovement * Mathf.Cos(t), _sizeOfMovement * Mathf.Sin(t), 0);
    }

}
