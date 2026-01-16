using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spaceshipSpriteRenderer;
    [SerializeField] private float speed;
    [SerializeField] private GameObject projectile;

    [SerializeField] private List<Transform> gunTransformList = new();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f));
        //transform.position += Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f);
    }

    private void Shoot()
    {
        if (gunTransformList == null || gunTransformList.Count == 0)
        {
            return;
        }

        foreach (Transform gun in gunTransformList)
        {
            Shoot(gun);
        }
    }

    private void Shoot(Transform gun)
    {
        GameObject clone = Instantiate(projectile, Vector3.zero, gun.rotation);
        clone.transform.SetParent(transform);
        clone.transform.localPosition = gun.localPosition;
    }
}