using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private SpecialProjectile projectile;
    [SerializeField] private float delay;
    [SerializeField] private int maxHealth;

    [Header("Patrol")]
    [SerializeField] private float startX;
    [SerializeField] private float endX;
    [SerializeField] private float speed;

    private bool _isMovingLeft;
    private float _count;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void Update()
    {
        if (transform.position.x >= endX)
        {
            _isMovingLeft = true;
        }

        if (transform.position.x <= startX)
        {
            _isMovingLeft = false;
        }
        Move();

        if (_count <= 0)
        {
            Shoot();
            _count = delay;
        }

        _count -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (target == null)
        {
            return;
        }

        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.Init(target);
    }

    private void Move()
    {
        //Vector3 moveVector;
        //if (_isMovingLeft)
        //{
        //    moveVector = Vector3.left;
        //}
        //else
        //{
        //    moveVector = Vector3.right;
        //}

        var moveVector = _isMovingLeft ? Vector3.left : Vector3.right;
        transform.Translate(speed * Time.deltaTime * moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
