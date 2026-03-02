using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    [SerializeField] private EnemyBullet bullet;
    [SerializeField] private float delay;

    private float timer = 0;

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
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
