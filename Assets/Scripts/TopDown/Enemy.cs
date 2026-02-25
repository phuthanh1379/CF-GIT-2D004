using UnityEngine;

namespace TopDown
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;

        private Transform _player;
        private bool _isChasing;

        private void Update()
        {
            if (_isChasing)
            {
                Vector2 direction = (_player.position - transform.position).normalized;
                rb.velocity = speed * direction;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Player")) 
            {
                _player = collision.transform;
                _isChasing = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                _isChasing = false;
            }
        }
    }
}