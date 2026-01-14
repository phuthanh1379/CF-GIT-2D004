using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spaceshipSpriteRenderer;
    [SerializeField] private float speed;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f));
        //transform.position += Time.deltaTime * speed * new Vector3(horizontal, vertical, 0f);
    }
}