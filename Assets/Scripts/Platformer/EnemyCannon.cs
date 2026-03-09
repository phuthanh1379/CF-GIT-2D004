using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    [SerializeField] private EnemyBullet bullet;
    [SerializeField] private float delay;
    [SerializeField] private ObjectPooling bulletPool;

    private float timer = 0;

    private void Start()
    {
        bulletPool.SetPrefab(bullet.gameObject);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Shoot();
            timer = delay;
        }

        timer -= Time.deltaTime;
    }

    private void Shoot()
    {
        var spawnedBullet = bulletPool.Get();
        spawnedBullet.transform.position = transform.position;
    }
}
