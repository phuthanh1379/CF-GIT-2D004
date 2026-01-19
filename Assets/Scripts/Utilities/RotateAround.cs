using System;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField, Range(-5f, 5f)] private float speed;
    [SerializeField, Tooltip("Point where object rotates around")] private Transform axisPoint;

    void Update()
    {
        transform.RotateAround(axisPoint.position, new Vector3(0f, 0f, 1f), speed);
    }
}