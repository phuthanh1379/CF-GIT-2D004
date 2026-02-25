using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpperGroundExit : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D tilemapCollider;
    [SerializeField] private TilemapCollider2D borderCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tilemapCollider.enabled = true;
        borderCollider.enabled = false;
        collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
}
