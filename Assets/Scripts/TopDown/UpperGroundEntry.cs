using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpperGroundEntry : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D tilemapCollider;
    [SerializeField] private TilemapCollider2D borderCollider;

    private void Start()
    {
        borderCollider.enabled = false;
        tilemapCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tilemapCollider.enabled = false;
        borderCollider.enabled = true;
        collision.GetComponent<SpriteRenderer>().sortingOrder = 15;
    }
}
