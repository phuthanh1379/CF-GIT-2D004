using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Spaceship player;

    [Header("Obstacles")]
    [SerializeField] private List<Obstacle> obstacleList = new();

    [Header("UI")]
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text bulletCountText;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerScoreText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        player.PlayerDead += OnPlayerDead;

        foreach (var obstacle in obstacleList)
        {
            obstacle.ObstacleDestroyed += OnObstacleDestroyed;
        }
    }

    private void OnDestroy()
    {
        player.PlayerDead -= OnPlayerDead;

        foreach (var obstacle in obstacleList)
        {
            obstacle.ObstacleDestroyed -= OnObstacleDestroyed;
        }
    }

    private void Start()
    {
        pauseMenuGameObject.SetActive(false);
        playerNameText.text = GameData.Instance.PlayerName;
        playerScoreText.text = $"{GameData.Instance.PlayerProfile.PlayerScore}";
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuGameObject.SetActive(true);
        }

        healthText.text = $"Health: {player.CurrentHealth}";
        healthBarImage.fillAmount = (float)player.CurrentHealth / player.HealthMax;
        bulletCountText.text = $"Bullet: {player.BulletCount}/{player.BulletMax}";
    }

    public void OnClickQuitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnPlayerDead()
    {
        gameOverUI.SetActive(true);
    }

    private void OnObstacleDestroyed(int score)
    {
        GameData.Instance.PlayerProfile.AddPlayerScore(score);
        playerScoreText.text = $"{GameData.Instance.PlayerProfile.PlayerScore}";
    }
}
