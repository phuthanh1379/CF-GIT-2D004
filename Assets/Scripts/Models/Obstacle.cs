using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int score;

    public event Action<int> ObstacleDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObstacleDestroyed?.Invoke(score);
        Destroy(gameObject);
    }
}
