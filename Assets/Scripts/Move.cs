using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), speed);
        //transform.RotateAround(Vector3.zero, new Vector3(0f, 0f, 1f), speed);
    }
}
