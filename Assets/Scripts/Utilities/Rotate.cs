using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), speed * Time.deltaTime);
    }
}
