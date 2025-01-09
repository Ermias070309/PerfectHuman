using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackAndForth : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float speed;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = PointB; 
    }

    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == PointB ? PointA : PointB;
        }
    }
}
