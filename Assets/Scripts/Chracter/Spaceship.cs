using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator animator;

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
    private int _currentHealth;
    private Color _baseColor;

    private void Awake()
    {
        _bulletCount = bulletMax;
        _baseColor = spriteRenderer.color;
        _currentHealth = maxHealth;
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
        animator.SetInteger("X", (int)horizontal);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetInteger("Y", (int)vertical);

        transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f));
        //transform.position += Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(this.gameObject);
        StartCoroutine(OnHit());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.LogError($"Trigger! {collision.gameObject.name}");
        StartCoroutine(OnHit());
    }

    private IEnumerator OnHit()
    {
        _currentHealth--;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = _baseColor;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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