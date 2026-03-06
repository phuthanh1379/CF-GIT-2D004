using UnityEngine;

public enum Direction
{
    Left,
    Right,
}

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private float speed;
    [SerializeField] private float xMax;
    [SerializeField] private Vector3 _basePosition;

    private void Update()
    {
        if (direction == Direction.Left)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    private void MoveRight()
    {
        if (transform.position.x >= xMax)
        {
            transform.position = _basePosition;
        }

        transform.Translate(speed * Time.deltaTime * new Vector3(1f, 0f, 0f));
    }

    private void MoveLeft()
    {
        if (transform.position.x <= xMax)
        {
            transform.position = _basePosition;
        }

        transform.Translate(speed * Time.deltaTime * new Vector3(-1f, 0f, 0f));
    }
}
