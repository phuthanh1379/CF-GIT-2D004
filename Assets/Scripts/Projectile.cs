using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector3(0f, 1f, 0f));
    }
}
