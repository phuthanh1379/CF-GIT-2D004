using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileValue value;
    //[SerializeField] private float speed;
    //[SerializeField] private float timeToLive;

    private void Start()
    {
        StartCoroutine(SelfDestroy(value.TimeToLive));
    }

    private void Update()
    {
        transform.Translate(value.Speed * Time.deltaTime * new Vector3(0f, 1f, 0f));
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}

[Serializable]
public class ProjectileValue
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;

    public float Speed => speed;
    public float TimeToLive => timeToLive;
}