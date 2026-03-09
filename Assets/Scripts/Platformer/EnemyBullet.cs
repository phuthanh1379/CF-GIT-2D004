using Platformer;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float startX;

    private bool _isDisable;
    private float _baseX;
    private ObjectPooling pool;

    private void OnEnable()
    {
        _isDisable = false;
        if (pool == null)
        {
            pool = FindObjectOfType<ObjectPooling>();
        }

        _baseX = transform.position.x;
        rb.velocity = new Vector3(startX, 0, 0);
        Invoke(nameof(SelfDestroy), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnHurt(transform.position.x);
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        if (_isDisable)
        {
            return;
        }

        _isDisable = true;
        if (pool == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            pool.AddToPool(this.gameObject);
        }
    }
}
