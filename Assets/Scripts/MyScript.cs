using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    [SerializeField] private float newY = 0.001f;
    [SerializeField] private float newZ = 0.001f;
    public float newScale = 1.001f;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = transform.position + new Vector3(0, newY, newZ);
        transform.localScale = transform.localScale * newScale;
    }
}
