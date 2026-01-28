using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBarImage;

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
    private Color _baseColor;

    public event Action PlayerDead;

    public int CurrentHealth { get; private set; }
    public int BulletCount { get; private set; }
    public int BulletMax => bulletMax;
    public int HealthMax => maxHealth;

    private void Awake()
    {
        BulletCount = bulletMax;
        _baseColor = spriteRenderer.color;
        CurrentHealth = maxHealth;
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
        healthBarImage.fillAmount = (float)CurrentHealth / HealthMax;
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
        CurrentHealth--;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = _baseColor;

        if (CurrentHealth <= 0)
        {
            PlayerDead?.Invoke();
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

        if (BulletCount <= 0)
        {
            if (_isReloading)
            {
                return;
            }

            StartCoroutine(Reload());
            return;
        }

        BulletCount--;
        AudioController.Instance.PlayShootSFX();
        foreach (Transform gun in gunTransformList)
        {
            Shoot(gun);
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        BulletCount = bulletMax;
        _isReloading = false;
    }

    private void Shoot(Transform gun)
    {
        var clone = Instantiate(projectile, gun.position, gun.rotation);
    }
}