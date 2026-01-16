using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShoot : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private SpecialProjectile meteor;
    [SerializeField] private float delay;

    private float _count;

    private void Update()
    {
        if (_count <= 0)
        {
            ShootMeteor();
            _count = delay;
        }

        _count -= Time.deltaTime;
    }

    private void ShootMeteor()
    {
        var clone = Instantiate(meteor, transform.position, Quaternion.identity);
        clone.Init(target);
    }
}
