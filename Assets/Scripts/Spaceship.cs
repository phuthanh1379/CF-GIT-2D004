using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float speed;
    [SerializeField] private Transform meteor;

    [Header("Shooting")]
    [SerializeField] private int bulletMax;
    [SerializeField] private float reloadTime;
    [SerializeField] private Projectile projectile;
    [SerializeField] private List<Transform> gunTransformList = new List<Transform>();

    [Header("Special Shooting")]
    [SerializeField] private SpecialProjectile specialProjectile;
    [SerializeField] private Transform target;
    [SerializeField] private Transform specialGunTransform;

    private bool _isReloading;
    private int _bulletCount;

    private void Awake()
    {
        _bulletCount = bulletMax;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpecialShoot();
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f));
        //transform.position += Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f);
    }

    private void SpecialShoot()
    {
        var clone = Instantiate(specialProjectile, specialGunTransform.position, specialGunTransform.rotation);
        clone.Init(target);
    }

    private void Shoot()
    {
        if (gunTransformList == null || gunTransformList.Count == 0)
        {
            return;
        }

        if (_bulletCount <= 0)
        {
            if (_isReloading)
            {
                return;
            }

            StartCoroutine(Reload());
            return;
        }

        _bulletCount--;
        foreach (Transform gun in gunTransformList)
        {
            Shoot(gun);
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        _bulletCount = bulletMax;
        _isReloading = false;
    }

    private void Shoot(Transform gun)
    {
        var clone = Instantiate(projectile, gun.position, gun.rotation);
    }
}