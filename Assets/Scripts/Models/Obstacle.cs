using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleProfile profile;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public event Action<int> ObstacleDestroyed;
    private int _score;

    private void Awake()
    {
        if (profile == null || profile.Sprite == null)
        {
            return;
        }

        spriteRenderer.sprite = profile.Sprite;
        _score = GetScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObstacleDestroyed?.Invoke(_score);
        Destroy(gameObject);
    }

    private int GetScore()
    {
        if (profile == null)
        {
            return 0;
        }

        return profile.Score;
    }
}
