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
        // Move towards the current target
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Check if close enough to switch target
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Switch target
            currentTarget = currentTarget == PointB ? PointA : PointB;

            // Flip the sprite (invert the X-axis scale)
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Flip the X scale
            transform.localScale = localScale;
        }
    }
}
