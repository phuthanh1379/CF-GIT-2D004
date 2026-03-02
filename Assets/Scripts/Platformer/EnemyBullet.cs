using System.Collections;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float startX;

    private float _baseX;

    private void Start()
    {
        _baseX = transform.position.x;
        rb.velocity = new Vector3(startX, 0, 0);
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnHurt(transform.position.x);
            Destroy(this.gameObject);
        }
    }
}
