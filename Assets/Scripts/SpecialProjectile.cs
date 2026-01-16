using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileValue value;

    private Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Start()
    {
        StartCoroutine(SelfDestroy(value.TimeToLive));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, 
            value.Speed * Time.deltaTime);
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
