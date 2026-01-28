using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData data;
    [SerializeField] private GameObject explosionFx;

    private MenuController menuController;

    private void Start()
    {
        StartCoroutine(SelfDestroy(data.TimeToLive));
    }

    private void Update()
    {
        transform.Translate(data.Speed * Time.deltaTime * new Vector3(0f, 1f, 0f));
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnityEngine.Debug.Log($"OnCollide with: {collision.gameObject.name}");
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

[Serializable]
public class ProjectileData
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;

    public float Speed => speed;
    public float TimeToLive => timeToLive;
}