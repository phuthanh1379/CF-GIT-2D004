using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileData data;

    private Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Start()
    {
        StartCoroutine(SelfDestroy(data.TimeToLive));
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position,
            data.Speed * Time.deltaTime);
        }
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
